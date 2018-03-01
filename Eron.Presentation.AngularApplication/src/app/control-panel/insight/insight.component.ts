import { Component, OnInit } from '@angular/core';
import { BreadCrump } from '../../base/models/breadCrump.model';
import { UrlNamePair } from '../../base/models/urlNamePair.model';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'insight',
  templateUrl: './insight.component.html',
  styleUrls: ['./insight.component.scss']
})
export class InsightComponent implements OnInit {

  breadCrump: BreadCrump;
  constructor() {
    this.breadCrump = {
      Title: 'آمار و ارقام',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      ThirdNode: new UrlNamePair('آمار و ارقام')
    } as BreadCrump;
  }

  ngOnInit() {
  }

}
