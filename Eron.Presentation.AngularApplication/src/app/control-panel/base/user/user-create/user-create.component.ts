import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { Common } from '../../../../base/common';
import { Config } from '../../../../app.config';

import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { UserService } from '../user.service';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { Button } from '../../../../base/models/button.model';
import { UserCreateModel } from './user-create.model';

import { CheckboxComponent } from '../../../../base/components/checkbox/checkbox.component';
import { forwardRef } from '@angular/core';
import { ProvinceService } from '../../../../base/services/province.service';
import { NotificationService } from '../../../../base/services/notification.service';
import { appVariables } from '../../../../app.constants';


@Component({
  selector: 'user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.scss']
})
export class UserCreateComponent implements OnInit {

  breadCrump: BreadCrump;
  userItems: any;
  user: UserCreateModel;
  roles: any[] = [];
  cityList: any[] = [];
  provinceList: any[] = [];
  submitting = false;
  constructor(
    private service: UserService,
    private titleService: Title,
    private provinceService: ProvinceService,
    private notificationService: NotificationService,
    private router: Router
  ) {

    this.titleService.setTitle(appVariables.defaultTitle + 'ثبت کاربر جدید');
    this.user = {} as UserCreateModel;
    this.breadCrump = {
      Title: 'ثبت کاربر جدید',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      SecondNode: new UrlNamePair('مدیریت کاربران', '/controlpanel/users'),
      ThirdNode: new UrlNamePair('ثبت کاربر جدید')
    } as BreadCrump;

    this.service.getRoles().subscribe(
      (response) => {
        this.roles = response;
      },
      (error) => {
        console.log(error);
      }
    );

    this.provinceList = this.provinceService.getProvincesList();

  }

  ngOnInit() {
  }

  onSubmit() {

    this.user.selectedRoles = this.roles.filter(x => x.selected).map(x => x.name);
    this.submitting = true;
    this.service.createUser(this.user).subscribe(
      (response) => {
        this.submitting = false;
        if (typeof(response.succeeded !== 'undefined') && response.succeeded === true) {
          this.notificationService.successfulOperation('ثبت کاربر جدید');
          this.router.navigateByUrl('/controlpanel/users');
        }else {
          this.notificationService.error('درخواست شما دچار مشکل شده است', 'مشکل در پردازش درخواست شما');
        }
      },
      (error) => {
        this.submitting = false;
        this.notificationService.serverError(error);
      }
    );
  }

  provinceChanged() {
    this.cityList = this.provinceService.getCityByName(this.user.provinceName);
  }
}
