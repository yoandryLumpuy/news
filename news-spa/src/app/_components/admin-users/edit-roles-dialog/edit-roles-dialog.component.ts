import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

export interface CheckBoxData{
  name: string;
  checked: boolean;
}

export interface DialogData {
  userName: string; 
  roles: Array<CheckBoxData>;
}

@Component({
  selector: 'app-edit-roles-dialog',
  templateUrl: './edit-roles-dialog.component.html',
  styleUrls: ['./edit-roles-dialog.component.css']
})
export class EditRolesDialogComponent implements OnInit {  
  
  constructor(private matDialogRef : MatDialogRef<EditRolesDialogComponent> , 
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  ngOnInit() {
  }

  onClose(){
    this.matDialogRef.close();
  }
}
