import { BannerStructureService } from './../../_services/banner-structure.service';
import { AuthService } from 'src/app/_services/auth.service';
import { switchMap } from 'rxjs/operators';
import { ManageUsersService } from './../../_services/manage-users.service';
import { CheckBoxData, EditRolesDialogComponent } from './edit-roles-dialog/edit-roles-dialog.component';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { defaultPaginationResult, PaginationResult } from 'src/app/_model/paginationResult.interface';
import { defaultQueryObject, QueryObject } from 'src/app/_model/queryObject.interface';
import { User } from 'src/app/_model/user.interface';
import { of } from 'rxjs';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit {
  paginationResult : PaginationResult<User> = defaultPaginationResult;
  queryObject : QueryObject = defaultQueryObject;  

  displayedColumns: string[] = ["Id", "UserName", "Roles", "Button"];
  dataSource = new MatTableDataSource(this.paginationResult.items);  
  user : User;

  constructor(private matDialog : MatDialog, 
      private manageUsersService: ManageUsersService,
      private authService : AuthService,
      private bannerStructureService: BannerStructureService) { } 

  ngOnInit(): void {
    this.bannerStructureService.updateBanner({centeredText: 'Administrate User Roles'});
    this.loadDataSourceData();
  }

  onPageChanged($event: PageEvent){   
    this.queryObject.page = $event.pageIndex;
    this.queryObject.pageSize = $event.pageSize;
  }

  loadDataSourceData(){
    this.manageUsersService.getUsers(this.queryObject)
    .subscribe(res => {
      this.paginationResult = res;
      this.updateDataSource();
    });
  }

  updateDataSource(){ 
    this.dataSource = new MatTableDataSource(this.paginationResult.items);
  } 
  
  editRoles(user: User){
    var checkBoxData = new Array<CheckBoxData>();
    this.manageUsersService.getAvailableRoles()
    .subscribe(roles => {
      var userRoles = user.roles;
      roles.forEach(elem => 
        checkBoxData.push({name: elem, checked: userRoles.includes(elem)}));
      this.matDialog.open(EditRolesDialogComponent,
        {
          minHeight: '300px',
          minWidth: '300px',
          data: {
            userName: user.userName,
            roles: checkBoxData
          }
        })
      .afterClosed()
      .pipe(switchMap((newRoles : Array<CheckBoxData>) => {
        if (!newRoles) return of();        
        return this.manageUsersService.updateRoles(user.userName, 
          newRoles.filter(elem => elem.checked).map(elem => elem.name));
      }))
      .subscribe(res => this.loadDataSourceData());   
     });    
  }
}
