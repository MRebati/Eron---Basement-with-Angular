import { Component, OnInit, OnDestroy } from '@angular/core';
import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { Button } from '../../../../base/models/button.model';
import { PageListModel } from './page-list.model';
import { PageService } from '../page.service';
import { ToastsManager } from 'ng2-toastr/src/toast-manager';
import { NotificationService } from '../../../../base/services/notification.service';
import { Subscribable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { Api } from '../../../../base/api';
import { PubSubService } from 'angular2-pubsub';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'page-list',
  templateUrl: './page-list.component.html',
  styleUrls: ['./page-list.component.scss']
})
export class PageListComponent implements OnInit, OnDestroy {

  breadCrump: BreadCrump;
  pageItems: PageListModel[] = [];
  deleteSubscription: Subscription;
  deleteSubscriptionName = 'pageDeleteSuccess';
  constructor(
    private service: PageService,
    private notificationService: NotificationService,
    private subService: PubSubService
  ) {
    this.breadCrump = {
      Title: 'مدیریت صفحات',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      ThirdNode: new UrlNamePair('مدیریت صفحات'),
      Button: [
        {
          ButtonText: 'ثبت صفحه',
          ButtonClass: 'btn btn-success btn-outline',
          ButtonIconClass: 'fa fa-save',
          Url: '/controlpanel/pages/create'
        } as Button
      ]
    } as BreadCrump;

    this.service.getAllPages().subscribe(
      (response: PageListModel[]) => {
        this.pageItems = response;
      },
      (error) => {
        this.notificationService.serverError();
        console.log(error);
      }
    );

    this.deleteSubscription = this.subService.$sub(this.deleteSubscriptionName).subscribe(
      (response) => {
        const index = this.pageItems.findIndex(x => x.id === response);
        this.pageItems.splice(index, 1);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.deleteSubscription.unsubscribe();
  }

  removeItem(item) {
    this.notificationService.deleteConfirmationAlert('صفحه ' + item.title, item.id, Api.page.default, this.deleteSubscriptionName);
  }
}
