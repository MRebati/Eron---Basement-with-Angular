import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { PageService } from '../page.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationService } from '../../../../base/services/notification.service';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { PageUpdateModel } from './page-update.model';
import { ViewChild } from '@angular/core';

declare var $: any;

@Component({
  selector: 'page-update',
  templateUrl: './page-update.component.html',
  styleUrls: ['./page-update.component.scss']
})
export class PageUpdateComponent implements OnInit {

  breadCrump: BreadCrump;
  form: FormGroup;
  initModel: PageUpdateModel;
  redirect: boolean;
  submitting: boolean;
  @ViewChild('summernote') summernoteElement;
  @ViewChild('tagsinput') tagsinputElement;
  constructor(
    private service: PageService,
    private fb: FormBuilder,
    private router: Router,
    private notificationService: NotificationService,
    private activatedRoute: ActivatedRoute
  ) {
    this.breadCrump = {
      Title: 'ویرایش صفحه',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      SecondNode: new UrlNamePair('مدیریت صفحات', '/controlpanel/pages'),
      ThirdNode: new UrlNamePair('ویرایش صفحه'),
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
      'id': [null, Validators.required],
      'title': [null, Validators.required],
      'slug': [null, Validators.required],
      'keywords': [null, null],
      'description': [null, null],
      'content': [null, Validators.required],
      'views': [null],
      'createDateTime': [null]
    });

    const entityId = Number.parseInt(this.activatedRoute.snapshot.paramMap.get('id'));
    this.service.getPageById(entityId).subscribe(
      (response) => {
        this.initModel = response;
        this.form.setValue(response);

        const summernoteElement = this.summernoteElement.nativeElement;
        const tagsinputElement = this.tagsinputElement.nativeElement;
        $(summernoteElement).parent().find('.note-editable').html(response.content);
        $(tagsinputElement).parent().find('.tagsinput').remove();
        $(tagsinputElement).tagsInput();
      },
      (error) => {
        this.notificationService.serverError();
        console.log(error);
      });

  }

  ngOnInit() {
  }

  save(form: any) {
    this.redirect = true;
    const model = form.value as PageUpdateModel;
    this.submitting = true;
    this.service.updatePage(model).subscribe(
      (response) => {
        this.submitting = false;
        this.notificationService.successfulOperation('ویرایش صفحه');
        if (this.redirect) {
          this.router.navigateByUrl('/controlpanel/pages');
        } else {
          this.form.reset();
        }
      },
      (error) => {
        this.submitting = false;
        this.notificationService.serverError();
        console.log(error);
      }
    );
  }
}
