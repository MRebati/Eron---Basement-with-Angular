import { Component, OnInit } from '@angular/core';
import { BreadCrump } from '../../../base/models/breadCrump.model';
import { UrlNamePair } from '../../../base/models/urlNamePair.model';
import { Button } from '../../../base/models/button.model';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit {

  breadCrump: BreadCrump;

  constructor() {
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
          Url: '/controlpanel/page/create'
        } as Button
      ]
    } as BreadCrump;
  }

  ngOnInit() {
  }

}
