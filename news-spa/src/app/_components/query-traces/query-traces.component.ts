import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-query-traces',
  templateUrl: './query-traces.component.html',
  styleUrls: ['./query-traces.component.css']
})
export class QueryTracesComponent implements OnInit {

  paginationResult : PaginationResult<User> = defaultPaginationResult;
  queryObject : QueryObject = defaultQueryObject;  

  displayedColumns: string[] = ["Id", "UserName", "Roles", "Button"];
  dataSource = new MatTableDataSource(this.paginationResult.items);  
  user : User;

  constructor(private matDialog : MatDialog, 
      private manageUsersService: ManageUsersService,
      private authService : AuthService) { } 

  ngOnInit(): void {
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
  
  editRoles(userName: string, roles: string[]){
    var checkBoxData = new Array<CheckBoxData>();
    this.manageUsersService.getAvailableRoles()
    .subscribe(roles => {
      var userName = this.authService.decodedToken().unique_name;
      var userRoles = this.authService.decodedToken().role as Array<string>;
      roles.forEach(elem => 
        checkBoxData.push({name: elem, checked: userRoles.includes(elem)}));
      this.matDialog.open(EditRolesDialogComponent,
        {
          minHeight: '300px',
          minWidth: '300px',
          data: {
            userName,
            roles: checkBoxData
          }
        })
      .afterClosed()
      .pipe(switchMap((newRoles : Array<CheckBoxData>) => {
        return this.manageUsersService.updateRoles(userName, 
          newRoles.filter(elem => elem.checked).map(elem => elem.name));
      }))
      .subscribe(res => this.loadDataSourceData());   
     });    
  }
}
