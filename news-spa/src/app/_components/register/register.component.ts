import { BannerStructureService } from './../../_services/banner-structure.service';
import { Router } from '@angular/router';
import { UserForRegister, defaultUserForRegister } from './../../_model/userForRegister';
import { NomenclatorsService } from './../../_services/nomenclators.service';
import { forkJoin, Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';
import { KeyValuePair } from 'src/app/_model/KeyValuePair';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {
  languages : KeyValuePair[];
  countries : KeyValuePair[];

  model : UserForRegister = defaultUserForRegister;  
  subscription: Subscription;
   
  constructor(private http : HttpClient,
    public authService : AuthService,
    private alertService: AlertService,
    private nomenclatorsService: NomenclatorsService,
    private bannerStructureService: BannerStructureService,
    private router : Router) { } 
    
  ngOnInit(): void {
    this.bannerStructureService.updateBanner({centeredText: 'Registration'});

    forkJoin([
      this.nomenclatorsService.getLanguages(),
      this.nomenclatorsService.getCountries()
    ]).subscribe(res => {
      this.languages = res[0];
      this.countries = res[1];
    }, 
    error => this.alertService.error('There was error retreiving data!'));
  }

  ngOnDestroy(): void {
    if (this.subscription) this.subscription.unsubscribe();
  }

  register(){
    if (this.subscription) this.subscription.unsubscribe();

    this.subscription = this.authService.register(this.model)
    .subscribe(() => {
      this.alertService.success("successfully registered!");       
    });
  } 
  
  onCancel(){
    if (this.authService.loggedIn)
      this.router.navigate(['news']);
    else
      this.router.navigate(['login']);
  }
}
