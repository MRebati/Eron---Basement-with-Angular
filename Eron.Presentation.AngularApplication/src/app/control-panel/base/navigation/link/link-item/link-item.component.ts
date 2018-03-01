import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { NavigationViewModel } from '../../navigation.view.model';
import { NotificationService } from '../../../../../base/services/notification.service';
import { Api } from '../../../../../base/api';

@Component({
  // tslint:disable-next-line:component-selector
  selector: '[app-link-item]',
  templateUrl: './link-item.component.html',
  styleUrls: ['./link-item.component.scss']
})
export class LinkItemComponent implements OnInit {

  @Input() item: NavigationViewModel;
  @Input() itemList: NavigationViewModel[];
  children: NavigationViewModel[];
  constructor(
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    if (this.itemList != null) {
      this.children = this.itemList.filter(x => x.parentId === this.item.id);
    }
  }

  deleteShow() {
    this.notificationService.deleteConfirmationAlert('لینک', this.item.id, Api.link.default, 'linkDeletedSuccess');
  }

  editShow() {
  }

}
