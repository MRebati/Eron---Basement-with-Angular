import { Component, OnInit, OnDestroy } from '@angular/core';
import { SlideListModel } from './slide-list.model';
import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { SliderService } from '../slider.service';
import { NotificationService } from '../../../../base/services/notification.service';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { Button } from '../../../../base/models/button.model';
import { Api } from '../../../../base/api';
import { Subscription } from 'rxjs/Subscription';
import { PubSubService } from 'angular2-pubsub';
import { CommonService } from '../../../../base/services/common.service';

@Component({
  selector: 'slider-list',
  templateUrl: './slider-list.component.html',
  styleUrls: ['./slider-list.component.scss']
})
export class SliderListComponent implements OnInit, OnDestroy {

  breadCrump: BreadCrump;
  slideItems: SlideListModel[] = [];
  deleteSubscription: Subscription;
  deleteSubscriptionName = 'itemDeleteSuccess';
  constructor(
    private service: SliderService,
    private notificationService: NotificationService,
    private commonService: CommonService,
    private subService: PubSubService
  ) {

    this.breadCrump = {
      Title: 'مدیریت اسلاید ها - بنر ها',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      ThirdNode: new UrlNamePair('مدیریت اسلاید - بنر'),
      Button: [
        {
          ButtonText: 'ثبت جدید',
          ButtonClass: 'btn btn-primary btn-outline',
          ButtonIconClass: 'fa fa-save',
          Url: '/controlpanel/sliders/create'
        } as Button
      ]
    } as BreadCrump;

    this.service.getList().subscribe(
      (response: SlideListModel[]) => {
        this.slideItems = response;
      },
      (error) => {
        this.notificationService.serverError();
      }
    );

    this.deleteSubscription = this.subService.$sub(this.deleteSubscriptionName).subscribe(
      (response) => {
        const index = this.slideItems.findIndex(x => x.id === response);
        this.slideItems.splice(index, 1);
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
    this.notificationService.deleteConfirmationAlert('بنر ' + item.title, item.id, Api.slider.default, this.deleteSubscriptionName);
  }
}
