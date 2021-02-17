import { NewsComponent } from './_components/news/news.component';
import { QueryTracesComponent } from './_components/query-traces/query-traces.component';
import { AdminUsersComponent } from './_components/admin-users/admin-users.component';
import { ContactListComponent } from './_components/contact-list/contact-list.component';
import { StringsLimitedPipe } from './_pipes/strings-limited.pipe';
import { ConfirmDialogComponent } from './_services/confirm-dialog/confirm-dialog.component';
import { AlertComponent } from './_services/alert/alert.component';
import { MyErrorHandler } from './error-handler';
import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from "@angular/common";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { UserWithRolesDirective } from './_directives/userWithRoles.directive';
import { appRoutingModule } from './app-routing.module';
import { AuthInterceptorService } from './_services/authInterceptor.service';

import {MatModule} from './_modules/mat.module';

import { MAT_SNACK_BAR_DATA, MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { UploadProgressInterceptorService } from './_services/upload-progress-interceptor.service';
import { LoginComponent } from './_components/login/login.component';

@NgModule({
  declarations: [					
      AppComponent,
      UserWithRolesDirective,
      AlertComponent, 
      ConfirmDialogComponent,
      StringsLimitedPipe,
      LoginComponent,      
      ContactListComponent,
      AdminUsersComponent,
      QueryTracesComponent,
      NewsComponent
   ],
  imports: [
    BrowserModule, 
    CommonModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    appRoutingModule,
    MatModule, 
    ReactiveFormsModule
  ],
  providers: [
    {provide: ErrorHandler, useClass: MyErrorHandler},
    {
      provide: HTTP_INTERCEPTORS, 
      useClass: AuthInterceptorService, 
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: UploadProgressInterceptorService,
      multi: true
    },
    {provide: MAT_SNACK_BAR_DATA, useValue: {}}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
