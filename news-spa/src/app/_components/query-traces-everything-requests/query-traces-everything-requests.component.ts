import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { Subscription } from 'rxjs';
import { NewsQueryTracerService } from './../../_services/news-query-tracer.service';
import { QueryObjectEverythingRequest } from './../../_model/query-object-everything-request';
import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { defaultPaginationResult, PaginationResult } from 'src/app/_model/paginationResult.interface';
import { defaultQueryObject, QueryObject } from 'src/app/_model/queryObject.interface';
import { MatSort, Sort } from '@angular/material/sort';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-query-traces-everything-requests',
  templateUrl: './query-traces-everything-requests.component.html',
  styleUrls: ['./query-traces-everything-requests.component.css']
})
export class QueryTracesEverythingRequestsComponent implements OnInit, OnDestroy {
  public SortByCreatedByUser = "CreatedByUser";
  public SortByCreatedAt = "CreatedAt";
  public SortByQ = "Q";
  public ColumnNameSources = "Sources";
  public ColumnNameDomains = "Domains";
  public SortByFromDatetime = "From";
  public SortByToDatetime = "To";
  public SortByLanguage = "Language"; 

  subscription : Subscription;
  

  paginationResult : PaginationResult<QueryObjectEverythingRequest> = defaultPaginationResult;
  queryObject : QueryObject = defaultQueryObject;  

  displayedColumns: string[] = [this.SortByCreatedByUser, this.SortByCreatedAt, this.SortByQ, 
                                this.ColumnNameSources, this.ColumnNameDomains, this.SortByFromDatetime, 
                                this.SortByToDatetime, this.SortByLanguage];
  dataSource = new MatTableDataSource(this.paginationResult.items);  

  @ViewChild(MatSort) sort: MatSort;

  constructor(private newsQueryTracerService: NewsQueryTracerService) { }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    if (this.subscription) this.subscription.unsubscribe();
  }

  ngOnInit() {         
    this.loadPaginatedData();      
  } 

  updateDataSource(){
    this.dataSource = new MatTableDataSource(this.paginationResult.items);
    this.dataSource.sort = this.sort;
  }

  onPageChanged($event: PageEvent){   
    this.queryObject.page = $event.pageIndex;
    this.queryObject.pageSize = $event.pageSize;
    this.loadPaginatedData();
  }    

  loadPaginatedData(){  
    if (!!this.subscription)  this.subscription.unsubscribe();
    this.subscription =
      this.newsQueryTracerService.getTracesEverythingRequests(this.queryObject)
      .subscribe(paginatedResult => {
        this.paginationResult = paginatedResult;
        console.log(this.paginationResult);
        this.updateDataSource();
      });
  }

  onSortChange($event : Sort){
      this.queryObject.sortBy = $event.active;
      this.queryObject.isSortAscending = $event.direction == 'asc';  
      this.queryObject.page = 0; 
      this.loadPaginatedData();
  }
}
