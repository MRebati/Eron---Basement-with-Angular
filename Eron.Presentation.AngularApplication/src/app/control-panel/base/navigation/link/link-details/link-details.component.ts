import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { NavigationService } from '../../navigation.service';
import { PubSubService } from 'angular2-pubsub';
import { NotificationService } from '../../../../../base/services/notification.service';
import { Validators } from '@angular/forms';
import { MenuCreateModel } from '../../menu/menu-create/menu-create.model';
import { SelectListService } from '../../../../../base/services/selectList.service';
import { SelectListItem } from '../../../../../base/models/SelectList.model';

@Component({
  selector: 'app-link-details',
  templateUrl: './link-details.component.html',
  styleUrls: ['./link-details.component.scss']
})
export class LinkDetailsComponent implements OnInit {

  form: FormGroup;
  targets: SelectListItem[];
  submitting: boolean;
  constructor(
    private fb: FormBuilder,
    private selectListService: SelectListService,
    private navigationService: NavigationService,
    private publishService: PubSubService,
    private notificationService: NotificationService
  ) {
    this.form = this.fb.group({
      // 'linkType': [null, Validators.required],
      // 'linkPlacement': [null, Validators.required],
      'url': [null, Validators.required],
      'linkText': [null, Validators.required],
      // 'urlTargetType': [null, Validators.required],
      'target': [null, Validators.required],
      // 'image': [null, Validators.required],
      // 'iconClass': [null, Validators.required]
    });

    this.selectListService
      .getSelectList('UrlTargetType')
      .subscribe(
      (response) => {
        this.targets = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  ngOnInit() {
  }

  onSubmitForm(model: MenuCreateModel) {
    this.submitting = true;
    this.navigationService.createMenuItem(model).subscribe(
      (response) => {
        this.submitting = false;
        this.notificationService.successfulOperation('ثبت پیوند');
        this.publishService.$pub('menuItemCreateSuccess', response);
      },
      (error) => {
        this.submitting = false;
        this.notificationService.serverError();
        console.log(error);
      }
    );
  }

}
