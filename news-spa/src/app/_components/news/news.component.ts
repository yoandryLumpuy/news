import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';
import { BannerStructureService } from './../../_services/banner-structure.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  constructor(private bannerStructureService: BannerStructureService,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
    this.bannerStructureService.updateBanner({centeredText: 'News'});
    if (!this.authService.loggedIn) this.router.navigate(['login']);
  }

}
