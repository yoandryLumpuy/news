import { Router } from '@angular/router';
import { BannerStructureService } from './../../_services/banner-structure.service';
import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  model : any = {userName:'', password: ''};  
  subscription: Subscription;
   
  constructor(
    public authService : AuthService,
    private alertService: AlertService,
    private bannerStructureService: BannerStructureService,
    private router : Router) { } 
    
  ngOnInit(): void {
    this.bannerStructureService.updateBanner({centeredText: 'Loging in'});
  }

  ngOnDestroy(): void {
    if (this.subscription) this.subscription.unsubscribe();
  }

  login(){
    if (this.subscription) this.subscription.unsubscribe();

    this.subscription = this.authService.login(this.model)
    .subscribe(() => {
      this.alertService.success("successfully logged!");  
    });    
  }  

  logout(){
    this.authService.logout();
    this.alertService.success("successful loggout!");    
  }

  onRegisterClick(){
    this.router.navigate(['register']);
  }
}
