import { NewsRequestEverything, defaultNewsRequestEverything } from './../../_model/news-request-everything';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { KeyValuePair } from 'src/app/_model/KeyValuePair';
import { ArticlesResult, defaultArticlesResult } from 'src/app/_model/ArticlesResult';
import { forkJoin, Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NomenclatorsService } from 'src/app/_services/nomenclators.service';
import { AlertService } from 'src/app/_services/alert.service';
import { NewsService } from 'src/app/_services/news.service';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-news-everything-search',
  templateUrl: './news-everything-search.component.html',
  styleUrls: ['./news-everything-search.component.css']
})
export class NewsEverythingSearchComponent implements OnInit, OnDestroy {
  searching = false;
  languages : KeyValuePair[];
  sortBys : KeyValuePair[];

  everythingRequestParameters : NewsRequestEverything = defaultNewsRequestEverything;
  articlesResult : ArticlesResult = defaultArticlesResult;
  
  newsServiceSubscription: Subscription;

  form = new FormGroup({
    q: new FormControl('', Validators.required),        
    from: new FormControl(''), 
    to: new FormControl(''),   
    language: new FormControl(''),
    sortBy: new FormControl('')
  });
 
  constructor(private nomenclatorsService: NomenclatorsService, 
    private alertService: AlertService, private newsService: NewsService) { }

  ngOnDestroy(): void {
    if (this.newsServiceSubscription) this.newsServiceSubscription.unsubscribe();
  }

  get q(){
    return this.form.controls['q'] as FormControl
  }

  get from(){
    return this.form.controls['from'] as FormControl
  }

  get to(){
    return this.form.controls['to'] as FormControl
  }

  get language(){
    return this.form.controls['language'] as FormControl
  }

  get sortBy(){
    return this.form.controls['sortBy'] as FormControl
  }

  ngOnInit() {
    forkJoin([
      this.nomenclatorsService.getLanguages(),
      this.nomenclatorsService.getSortbys()
    ]).subscribe(res => {
      this.languages = res[0];
      this.sortBys = res[1];
    }, 
    error => this.alertService.error('There was error retreiving data!'));

    this.q.valueChanges.subscribe(value => {        
        this.loadEverything();
    });

    this.from.valueChanges.subscribe(value => {        
      this.loadEverything();
    });

    this.to.valueChanges.subscribe(value => {        
      this.loadEverything();
    });

    this.language.valueChanges.subscribe(value => {        
      this.loadEverything();
    });

    this.sortBy.valueChanges.subscribe(value => {        
      this.loadEverything();
    });
  }

  loadEverything(){
    this.everythingRequestParameters = {
      ...this.everythingRequestParameters,
      ...this.form.value
    }

    if (this.newsServiceSubscription) this.newsServiceSubscription.unsubscribe();
    this.newsServiceSubscription = this.newsService.getEverything(this.everythingRequestParameters)
    .subscribe(res =>
      this.articlesResult = res);
  };

  onPageChanged($event: PageEvent){   
    this.everythingRequestParameters.page = $event.pageIndex;
    this.everythingRequestParameters.pageSize = $event.pageSize;
    this.loadEverything();
  }
}
