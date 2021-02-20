import { ArticlesResult, defaultArticlesResult } from './../../_model/ArticlesResult';
import { NewsService } from './../../_services/news.service';
import { NewsRequestTopheadlines, defaultNewsRequestTopheadlines } from './../../_model/news-request-topheadlines';
import { AlertService } from './../../_services/alert.service';
import { KeyValuePair } from './../../_model/KeyValuePair';
import { NomenclatorsService } from './../../_services/nomenclators.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { forkJoin, Observable, Subscription } from 'rxjs';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-news-topheadlines',
  templateUrl: './news-topheadlines.component.html',
  styleUrls: ['./news-topheadlines.component.css']
})
export class NewsTopheadlinesComponent implements OnInit, OnDestroy {
  searching = false;
  categories : KeyValuePair[];
  languages : KeyValuePair[];
  countries : KeyValuePair[];

  topheadlinesRequestParameters : NewsRequestTopheadlines = defaultNewsRequestTopheadlines;
  articlesResult : ArticlesResult = defaultArticlesResult;
  
  newsServiceSubscription: Subscription;

  form = new FormGroup({
    q: new FormControl('', Validators.required),        
    category: new FormControl(''), 
    language: new FormControl(''),   
    country: new FormControl('')
  });
 
  constructor(private nomenclatorsService: NomenclatorsService, 
    private alertService: AlertService, private newsService: NewsService) { }

  ngOnDestroy(): void {
    if (this.newsServiceSubscription) this.newsServiceSubscription.unsubscribe();
  }

  get q(){
    return this.form.controls['q'] as FormControl
  }

  get category(){
    return this.form.controls['category'] as FormControl
  }

  get language(){
    return this.form.controls['language'] as FormControl
  }

  get country(){
    return this.form.controls['country'] as FormControl
  }

  ngOnInit() {
    forkJoin([
      this.nomenclatorsService.getCategories(),
      this.nomenclatorsService.getLanguages(),
      this.nomenclatorsService.getCountries()
    ]).subscribe(res => {
      this.categories = res[0];
      this.languages = res[1];
      this.countries = res[2];
    }, 
    error => this.alertService.error('There was error retreiving data!'));

    this.q.valueChanges.subscribe(value => {        
        this.loadTopheadlines();
    });

    this.category.valueChanges.subscribe(value => {        
      this.loadTopheadlines();
    });

    this.language.valueChanges.subscribe(value => {        
      this.loadTopheadlines();
    });

    this.country.valueChanges.subscribe(value => {        
      this.loadTopheadlines();
    });
  }

  loadTopheadlines(){
    this.topheadlinesRequestParameters = {
      ...this.topheadlinesRequestParameters,
      ...this.form.value
    }

    if (this.newsServiceSubscription) this.newsServiceSubscription.unsubscribe();
    this.newsServiceSubscription = this.newsService.getTopheadlines(this.topheadlinesRequestParameters)
    .subscribe(res =>
      this.articlesResult = res);
  };
 
  onPageChanged($event: PageEvent){   
    this.topheadlinesRequestParameters.page = $event.pageIndex;
    this.topheadlinesRequestParameters.pageSize = $event.pageSize;
    this.loadTopheadlines();
  }
}
