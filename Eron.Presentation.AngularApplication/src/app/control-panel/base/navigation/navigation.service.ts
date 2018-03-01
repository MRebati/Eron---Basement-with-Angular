
import { Injectable } from '@angular/core';
import { FooterCreateModel } from './footer/footer-create/footer-create.model';
import { PubSubService } from 'angular2-pubsub';
import { HttpClient } from '../../../base/services/app.http.service';
import { Api } from '../../../base/api';
import { NavigationInputModel } from './navigation.input.model';

@Injectable()
export class NavigationService {

  constructor(
    private http: HttpClient
  ) {

  }

  getFooterItems() {
    return this.http.get(Api.link.default + '1');
  }

  getFooterItemsAsTree() {
    return this.http.get(Api.link.tree + '1');
  }

  createFooterItem(model: FooterCreateModel) {
    const navigationModel: NavigationInputModel = model;
    navigationModel.linkPlacement = 1;
    return this.http.post(Api.link.default, navigationModel);
  }

  getMenuItems() {
    return this.http.get(Api.link.default + '0');
  }


  getMenuItemsAsTree() {
    return this.http.get(Api.link.tree + '0');
  }

  createMenuItem(model: FooterCreateModel) {
    const navigationModel: NavigationInputModel = model;
    navigationModel.linkPlacement = 0;
    return this.http.post(Api.link.default, model);
  }

  reOrderItems(model: any) {
    return this.http.post(Api.link.reOrder, model);
  }

}
