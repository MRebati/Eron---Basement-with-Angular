import { Component, OnInit } from '@angular/core';
import { BreadCrump } from '../../base/models/breadCrump.model';
import { UrlNamePair } from '../../base/models/urlNamePair.model';

@Component({
  selector: 'support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.scss']
})
export class SupportComponent implements OnInit {

  breadCrump: BreadCrump;
  constructor() {
    this.breadCrump = {
      Title: 'پشتیبانی',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      ThirdNode: new UrlNamePair('پشتیبانی')
    } as BreadCrump;
  }

  ngOnInit() {
  }

}
