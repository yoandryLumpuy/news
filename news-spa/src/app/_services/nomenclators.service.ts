import { KeyValuePair } from './../_model/KeyValuePair';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NomenclatorsService {

  constructor(private http : HttpClient) { }

  getCountries(){
    return this.http.get<KeyValuePair[]>(`${environment.baseUrl}nomenclator/countries`);
  }

  getCategories(){
    return this.http.get<KeyValuePair[]>(`${environment.baseUrl}nomenclator/categories`);
  }

  getLanguages(){
    return this.http.get<KeyValuePair[]>(`${environment.baseUrl}nomenclator/languages`);
  }

  getSortbys(){
    return this.http.get<KeyValuePair[]>(`${environment.baseUrl}nomenclator/sortbys`);
  }
}
