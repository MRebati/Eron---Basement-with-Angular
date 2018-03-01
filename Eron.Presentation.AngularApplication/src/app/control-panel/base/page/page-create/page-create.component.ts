import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { Button } from '../../../../base/models/button.model';
import { PageService } from '../page.service';
import { PageCreateModel } from './page-create.model';
import { ToastsManager } from 'ng2-toastr/src/toast-manager';
import { Router } from '@angular/router';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'page-create',
  templateUrl: './page-create.component.html',
  styleUrls: ['./page-create.component.scss']
})
export class PageCreateComponent implements OnInit {

  breadCrump: BreadCrump;
  form: FormGroup;
  redirect: boolean;
  submitting: boolean;
  constructor(
    private service: PageService,
    private fb: FormBuilder,
    private router: Router,
    public toastr: ToastsManager
  ) {
    this.breadCrump = {
      Title: 'ثبت صفحه جدید',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      SecondNode: new UrlNamePair('مدیریت صفحات', '/controlpanel/pages'),
      ThirdNode: new UrlNamePair('ثبت صفحه جدید'),
      // Button: [
      //   {
      //     ButtonText: 'ثبت صفحه',
      //     ButtonClass: 'btn btn-success btn-outline',
      //     ButtonIconClass: 'fa fa-save',
      //     Url: '/controlpanel/pages/create'
      //   } as Button
      // ]
    } as BreadCrump;

    this.form = fb.group({
      'title': [null, Validators.required],
      'slug': [null, Validators.required],
      'keywords': [null, null],
      'description': [null, null],
      'content': [null, Validators.required],
    });

  }


  ngOnInit() {
  }

  save(form: any) {
    this.redirect = true;
    const model = form.value as PageCreateModel;
    this.submitting = true;
    this.service.createPage(model).subscribe(
      (response) => {
        this.submitting = false;
        this.toastr.success('عملیات ثبت صفحه جدید با موفقیت انجام شد', 'عملیات موفق!');
        if (this.redirect) {
          this.router.navigateByUrl('/controlpanel/pages');
        } else {
          this.form.reset();
        }
      },
      (error) => {
        this.submitting = false;
        this.toastr.error('عملیات ثبت صفحه جدید با مشکل مواجه شد', 'عملیات ناموفق!');
        console.log(error);
      }
    );
  }

  saveAndNew(form: any) {
    this.redirect = false;
    const model = form.value as PageCreateModel;
    this.submitting = true;
    this.service.createPage(model).subscribe(
      (response) => {
        this.submitting = false;
        this.toastr.success('عملیات ثبت صفحه جدید با موفقیت انجام شد', 'عملیات موفق!');
        if (this.redirect) {
          this.router.navigateByUrl('/controlpanel/pages');
        } else {
          this.form.reset();
        }
      },
      (error) => {
        this.submitting = false;
        this.toastr.error('عملیات ثبت صفحه جدید با مشکل مواجه شد', 'عملیات ناموفق!');
        console.log(error);
      }
    );
  }

}
