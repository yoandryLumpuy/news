import { BannerStructure, defaultBannerStructure } from './../../_model/Constants';
import { BannerStructureService } from './../../_services/banner-structure.service';
import { AuthService } from './../../_services/auth.service';
import { Component, ViewChild, OnInit, ChangeDetectorRef } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable, Subscription } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent implements OnInit{

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  bannerStructure: BannerStructure = defaultBannerStructure;  
  bannerStructureServiceSubscription : Subscription;

  constructor(private breakpointObserver: BreakpointObserver, public authService : AuthService,
     private bannerStructureService: BannerStructureService, private cdRef: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.bannerStructureServiceSubscription 
      = this.bannerStructureService.observer.subscribe(res => {
        this.bannerStructure = res;
        this.cdRef.detectChanges();
      });
  }
}
