import { BannerStructureService } from './../../_services/banner-structure.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-query-traces',
  templateUrl: './query-traces.component.html',
  styleUrls: ['./query-traces.component.css']
})
export class QueryTracesComponent implements OnInit{
  constructor(private bannerStructureService: BannerStructureService){}

  ngOnInit(): void {
    this.bannerStructureService.updateBanner({centeredText: 'Search Traces'});
  }
}
