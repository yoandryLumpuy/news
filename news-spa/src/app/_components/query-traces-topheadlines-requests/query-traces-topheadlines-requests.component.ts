import { QueryObjectTopheadingRequest } from './../../_model/query-object-topheading-request';
import { Component, OnInit, ViewChild } from '@angular/core';
import { defaultPaginationResult, PaginationResult } from 'src/app/_model/paginationResult.interface';
import { defaultQueryObject, QueryObject } from 'src/app/_model/queryObject.interface';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { NewsQueryTracerService } from 'src/app/_services/news-query-tracer.service';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-query-traces-topheadlines-requests',
  templateUrl: './query-traces-topheadlines-requests.component.html',
  styleUrls: ['./query-traces-topheadlines-requests.component.css']
})
export class QueryTracesTopheadlinesRequestsComponent implements OnInit {
  public SortByCreatedByUser = "CreatedByUser";
  public SortByCreatedAt = "CreatedAt";
  public SortByQ = "Q";
  public ColumnNameSources = "Sources";
  public SortByLanguage = "Language";  
  public SortByCategory = "Category";
  public SortByCountry = "Country";
        

  paginationResult : PaginationResult<QueryObjectTopheadingRequest> = defaultPaginationResult;
  queryObject : QueryObject = defaultQueryObject;  

  displayedColumns: string[] = [this.SortByCreatedByUser, this.SortByCreatedAt, this.SortByQ, 
                                this.ColumnNameSources, this.SortByLanguage, this.SortByCategory,
                                this.SortByCountry];
  dataSource = new MatTableDataSource(this.paginationResult.items);  

  @ViewChild(MatSort) sort: MatSort;

  constructor(private newsQueryTracerService: NewsQueryTracerService) { }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    
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
    this.newsQueryTracerService.getTracesTopheadlinesRequests(this.queryObject)
    .subscribe(paginatedResult => {
      this.paginationResult = paginatedResult;
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
