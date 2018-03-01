import { Component, OnInit } from '@angular/core';
import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { UserService } from '../user.service';
import { Title } from '@angular/platform-browser';
import { ProvinceService } from '../../../../base/services/province.service';
import { NotificationService } from '../../../../base/services/notification.service';
import { Config } from '../../../../app.config';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { ActivatedRoute } from '@angular/router';
import { UserUpdateModel } from '../user-update/user-update.model';
import { appVariables } from '../../../../app.constants';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {

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
    private activatedRoute: ActivatedRoute
  ) {

    this.titleService.setTitle(appVariables.defaultTitle + 'نمایش اطلاعات کاربر');
    this.user = {} as UserUpdateModel;
    this.breadCrump = {
      Title: 'نمایش اطلاعات کاربر',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      SecondNode: new UrlNamePair('مدیریت کاربران', '/controlpanel/users'),
      ThirdNode: new UrlNamePair('نمایش اطلاعات کاربر')
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
    let userId;

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

  provinceChanged() {
    this.cityList = this.provinceService.getCityByName(this.user.provinceName);
  }
}
