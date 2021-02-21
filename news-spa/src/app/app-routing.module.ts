import { RegisterComponent } from './_components/register/register.component';
import { NewsComponent } from './_components/news/news.component';
import { AdminUsersComponent } from './_components/admin-users/admin-users.component';
import { QueryTracesComponent } from './_components/query-traces/query-traces.component';
import { AuthGuardService } from './_guards/authGuard.service';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { LoginComponent } from './_components/login/login.component';

const routes: Routes = [
  {path: '', component: NewsComponent}, 
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuardService],
    children:[
      {path: 'news', component: NewsComponent},
      {path: 'traces', component: QueryTracesComponent, data: {roles: ["Admin"]}},
      {path: 'users', component: AdminUsersComponent, data: {roles: ["Admin"]}}    
    ]
  },
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: '**', redirectTo: '', pathMatch: 'full'}       
];

@NgModule({
  imports:[
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class appRoutingModule {}


