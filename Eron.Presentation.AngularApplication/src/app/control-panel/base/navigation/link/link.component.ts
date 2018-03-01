import { Component, OnInit } from '@angular/core';
import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { Button } from '../../../../base/models/button.model';
import { PubSubService } from 'angular2-pubsub';
import { OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
import { Subscription } from 'rxjs/Subscription';
import { NavigationViewModel } from '../navigation.view.model';
import { NavigationService } from '../navigation.service';
import { NotificationService } from '../../../../base/services/notification.service';

@Component({
  selector: 'link-component',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class LinkComponent implements OnInit, OnDestroy {

  breadCrump: BreadCrump;
  updatingFooterLink: boolean;
  viewingFooterLink: boolean;
  footerListChangeSubsrciption: Subscription;
  footerItems: NavigationViewModel[];
  parentFooterItems: NavigationViewModel[];

  updatingMenuLink: boolean;
  viewingMenuLink: boolean;
  menuListChangeSubsrciption: Subscription;
  linkDeletedSubscription: Subscription;
  menuItems: NavigationViewModel[];
  parentMenuItems: NavigationViewModel[];
  constructor(
    private subscribeService: PubSubService,
    private navigationService: NavigationService,
    private notificationService: NotificationService
  ) {
    this.breadCrump = {
      Title: 'مدیریت لینک ها',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      ThirdNode: new UrlNamePair('مدیریت لینک ها'),
      // Button: [
      //   {
      //     ButtonText: 'ثبت تعرفه',
      //     ButtonClass: 'btn btn-success btn-outline',
      //     ButtonIconClass: 'fa fa-save',
      //     Url: '/controlpanel/tariff/create'
      //   } as Button
      // ]
    } as BreadCrump;

    this.footerListChangeSubsrciption = this.subscribeService.$sub('footerItemCreateSuccess').subscribe(
      (response) => {
        const footerItem = response;
        this.footerItems.push(footerItem);
        this.parentFooterItems.push(footerItem);
      }
    );

    this.menuListChangeSubsrciption = this.subscribeService.$sub('menuItemCreateSuccess').subscribe(
      (response) => {
        const menuItem = response;
        this.menuItems.push(menuItem);
        this.parentMenuItems.push(menuItem);
      }
    );

    this.linkDeletedSubscription = this.subscribeService.$sub('linkDeletedSuccess').subscribe(
      (response) => {
        const linkItem = response;
        let foundItemIndex = this.menuItems.findIndex(x => x.id === linkItem.id);
        if (foundItemIndex == null) {
          foundItemIndex = this.footerItems.findIndex(x => x.id === linkItem.id);
          this.footerItems.splice(foundItemIndex, 1);
        } else {
          this.menuItems.splice(foundItemIndex, 1);
        }
      }
    );
  }

  ngOnInit() {
    this.navigationService.getFooterItems().subscribe(
      (response) => {
        this.footerItems = response;
        this.parentFooterItems = response.filter(x => x.parentId == null);
      },
      (error) => {
        console.log(error);
        this.notificationService.serverError();
      }
    );

    this.navigationService.getMenuItems().subscribe(
      (response) => {
        this.menuItems = response;
        this.parentMenuItems = response.filter(x => x.parentId == null);
      },
      (error) => {
        console.log(error);
        this.notificationService.serverError();
      }
    );
  }

  ngOnDestroy() {
    this.footerListChangeSubsrciption.unsubscribe();
    this.menuListChangeSubsrciption.unsubscribe();
    this.linkDeletedSubscription.unsubscribe();
  }

  onUpdateFooterItem() {
    this.viewingFooterLink = false;
    this.updatingFooterLink = !this.updatingFooterLink;
  }

  onUpdateMenuItem() {
    this.viewingFooterLink = false;
    this.updatingFooterLink = !this.updatingFooterLink;
  }

  onViewFooterItem() {
    this.updatingFooterLink = false;
    this.viewingFooterLink = !this.viewingFooterLink;
  }

  onViewMenuItem() {
    this.updatingFooterLink = false;
    this.viewingFooterLink = !this.viewingFooterLink;
  }

  onFooterListChanges($event) {
    this.navigationService.reOrderItems($event).subscribe(
      (response) => {
        this.notificationService.successfulOperation('تغییرات منو ها');
      },
      (error) => {
        console.log(error);
        this.notificationService.serverError();
      }
    );
  }

  onMenuListChanges($event) {
    this.navigationService.reOrderItems($event).subscribe(
      (response) => {
        this.notificationService.successfulOperation('تغییرات منو ها');
      },
      (error) => {
        console.log(error);
        this.notificationService.serverError();
      }
    );
  }
}
