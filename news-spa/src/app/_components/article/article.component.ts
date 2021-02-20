import { Subscription } from 'rxjs';
import { BreakpointObserverService } from '../../_services/breakpoint-observer.service';
import { Component, Input, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { Article } from 'src/app/_model/Articles';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit, OnDestroy {
  @Input('article') article: Article;

  inSmallScreen = false;
  subscription : Subscription;

  constructor(private breakpointObserverService: BreakpointObserverService) { }  

  ngOnDestroy(): void {
    if (!!this.subscription) this.subscription.unsubscribe();
  }

  ngOnInit() {

    this.subscription = this.breakpointObserverService.inSmallScreen
      .subscribe(inSmallScreen => this.inSmallScreen = inSmallScreen);
  } 
}
