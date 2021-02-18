import { QueryObject } from '../_model/queryObject.interface';
import { AlertService } from './alert.service';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { PaginationResult } from '../_model/paginationResult.interface';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { User } from '../_model/user.interface';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ManageUsersService {

  constructor(private http: HttpClient, private authService: AuthService, 
    private router : Router, private alertService: AlertService) { }

  getUsers(queryObject: QueryObject) : Observable<PaginationResult<User>>{           
    return this.http.get<PaginationResult<User>>(environment.baseUrl + "admin/users?"+ this.queryObjectToString(queryObject));                
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

  updateRoles(userName : string, roles: string[]){
    return this.http.post(environment.baseUrl + 'admin/editRoles/' + userName, {roles});
  }

  getAvailableRoles(): Observable<string[]>{
    return this.http.get<string[]>(environment.baseUrl + 'admin/roles');
  } 
}
