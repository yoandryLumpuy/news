import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BannerStructure, defaultBannerStructure } from '../_model/Constants';

@Injectable({
  providedIn: 'root'
})
export class BannerStructureService {    
  public observer : BehaviorSubject<BannerStructure> = new BehaviorSubject<BannerStructure>(defaultBannerStructure);

  constructor() { }

  updateBanner(bannerStructure : BannerStructure){    
    this.observer.next(bannerStructure);
  }  
}
