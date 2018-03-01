import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Config } from '../../../../app.config';

import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';

import { UserService } from '../user.service';
import { ProvinceService } from '../../../../base/services/province.service';
import { NotificationService } from '../../../../base/services/notification.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { UserUpdateModel } from './user-update.model';
import { appVariables } from '../../../../app.constants';

@Component({
  selector: 'user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.scss']
})
export class UserUpdateComponent implements OnInit {

  breadCrump: BreadCrump;
  userItems: any;
  user: UserUpdateModel;
  roles: any[] = [];
  cityList: any[] = [];
  provinceList: any[] = [];
  submitting = false;
  constructor(
    private service: UserService,
    private titleService: Title,
    private provinceService: ProvinceService,
    private notificationService: NotificationService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {

    this.titleService.setTitle(appVariables.defaultTitle + 'ویرایش کاربر');
    this.user = {} as UserUpdateModel;
    this.breadCrump = {
      Title: 'ویرایش کاربر',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      SecondNode: new UrlNamePair('مدیریت کاربران', '/controlpanel/users'),
      ThirdNode: new UrlNamePair('ویرایش کاربر')
    } as BreadCrump;

    this.service.getRoles().subscribe(
      (response) => {
        this.roles = response;
      },
      (error) => {
        console.log(error);
      }
    );

    let userId: string;
    this.provinceList = this.provinceService.getProvincesList();

    this.activatedRoute.params.subscribe(
      param => {
        userId = param['id'];
        this.service.getUserFullInformationById(userId).subscribe(
          (response) => {
            this.user = response;
            this.service.getUserRoles(userId).subscribe(
              (roleResponse) => {
                const roleIds = roleResponse.map(x => x.id);
                this.roles.forEach(x => {
                  if (roleIds.indexOf(x.id) !== -1) {
                    x.selected = true;
                  }
                });
              }
            );
            this.provinceChanged();
          }
        );
      }
    );
  }

  ngOnInit() {
  }

  onSubmit() {

    this.user.selectedRoles = this.roles.filter(x => x.selected).map(x => x.name);
    this.submitting = true;
    this.service.updateUserAsAdmin(this.user).subscribe(
      (response) => {
        this.submitting = false;
        if (response === true) {
          this.notificationService.successfulOperation('ویرایش کاربر');
          this.router.navigateByUrl('/controlpanel/users');
        } else {
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
