import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AbstractControl } from '@angular/forms/src/model';
import { FooterCreateModel } from './footer-create.model';
import { NavigationService } from '../../navigation.service';
import { ToastsManager } from 'ng2-toastr/src/toast-manager';
import { PubSubService } from 'angular2-pubsub';
import { NotificationService } from '../../../../../base/services/notification.service';
import { SelectListItem } from '../../../../../base/models/SelectList.model';
import { SelectListService } from '../../../../../base/services/selectList.service';

@Component({
  selector: 'app-footer-create',
  templateUrl: './footer-create.component.html',
  styleUrls: ['./footer-create.component.scss']
})
export class FooterCreateComponent implements OnInit {

  form: FormGroup;
  targets: SelectListItem[];
  submitting: boolean;
  constructor(
    private fb: FormBuilder,
    public toastr: ToastsManager,
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

  onSubmitForm(model: FooterCreateModel) {
    this.submitting = true;
    this.navigationService.createFooterItem(model).subscribe(
      (response) => {
        this.submitting = false;
        this.notificationService.successfulOperation('ثبت پیوند');
        this.publishService.$pub('footerItemCreateSuccess', response);
      },
      (error) => {
        this.submitting = false;
        this.toastr.error('', '');
        console.log(error);
      }
    );
  }

}
