import { PaginationResult } from './../_model/paginationResult.interface';
import { QueryObjectTopheadingRequest } from './../_model/query-object-topheading-request';
import { QueryObject } from 'src/app/_model/queryObject.interface';
import { QueryObjectEverythingRequest } from './../_model/query-object-everything-request';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NewsQueryTracerService {

  constructor(private http : HttpClient) { }

  getTracesEverythingRequests(queryObject : QueryObject){
    return this.http.get<PaginationResult<QueryObjectEverythingRequest>>
           (`${environment.baseUrl}queriesTracer/everythingRequest?${this.queryObjectToString(queryObject)}`);
  }

  getTracesTopheadlinesRequests(queryObject : QueryObject){
    return this.http.get<PaginationResult<QueryObjectTopheadingRequest>>
           (`${environment.baseUrl}queriesTracer/topheadlinesRequest?${this.queryObjectToString(queryObject)}`);
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
