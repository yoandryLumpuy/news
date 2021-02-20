import { NewsRequestTopheadlines } from './../_model/news-request-topheadlines';
import { NewsRequestEverything } from './../_model/news-request-everything';
import { ArticlesResult } from './../_model/ArticlesResult';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  constructor(private http : HttpClient) { }

  getEverything(queryObject : NewsRequestEverything){
    return this.http.get<ArticlesResult>
           (`${environment.baseUrl}news/everything?${this.queryObjectToString(queryObject)}`);
  }

  getTopheadlines(queryObject : NewsRequestTopheadlines){
    return this.http.get<ArticlesResult>
           (`${environment.baseUrl}news/topheadlines?${this.queryObjectToString(queryObject)}`);
  }

  queryObjectToString(queryObject : any) : string{
    var parts : string[] = [];
    if (!!queryObject){
      for(let prop in queryObject){
        var value = queryObject[prop];
        if (value != null && value != undefined) 
          parts.push(encodeURIComponent(prop) + '='+ encodeURIComponent(queryObject[prop]))
      }
    }
    return parts.join('&');
  } 
}
