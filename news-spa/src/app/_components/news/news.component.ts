import { BannerStructureService } from './../../_services/banner-structure.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  constructor(private bannerStructureService: BannerStructureService) { }

  ngOnInit() {
    this.bannerStructureService.updateBanner({centeredText: 'News'});
  }

}
