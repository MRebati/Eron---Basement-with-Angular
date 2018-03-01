import { Component, OnInit } from '@angular/core';
import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { Button } from '../../../../base/models/button.model';
import { UserService } from '../user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  breadCrump: BreadCrump;
  userItems: any;
  constructor(
    private service: UserService,
    private router: Router
  ) {
    this.breadCrump = {
      Title: 'مدیریت کاربران',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      ThirdNode: new UrlNamePair('مدیریت کاربران'),
      Button: [
        {
          ButtonText: 'ثبت کاربر جدید',
          ButtonClass: 'btn btn-success btn-outline',
          ButtonIconClass: 'fa fa-save',
          Url: '/controlpanel/users/create'
        } as Button
      ]
    } as BreadCrump;

    this.service.getUsers().subscribe(
      (response) => {
        this.userItems = response;
      },
      (error) => {
        console.log(error);
      }
    );
   }

  ngOnInit() {
  }

  onDelete(item: any) {

  }
}
