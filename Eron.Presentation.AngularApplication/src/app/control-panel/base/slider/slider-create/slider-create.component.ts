import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { BreadCrump } from '../../../../base/models/breadCrump.model';
import { SliderService } from '../slider.service';
import { NotificationService } from '../../../../base/services/notification.service';
import { UrlNamePair } from '../../../../base/models/urlNamePair.model';
import { SliderCreateModel } from './slider-create.model';
import { EronFile } from '../../../../base/models/EronFile.model';
import { FileService } from '../../../../base/services/file.service';
import { Api } from '../../../../base/api';

@Component({
  selector: 'slider-create',
  templateUrl: './slider-create.component.html',
  styleUrls: ['./slider-create.component.scss']
})
export class SliderCreateComponent implements OnInit {
  groupName: string;
  breadCrump: BreadCrump;
  form: FormGroup;
  redirect: boolean;
  submitting: boolean;
  sliderList: SliderCreateModel[] = [];
  description: any;
  constructor(
    private service: SliderService,
    private fb: FormBuilder,
    private router: Router,
    private fileService: FileService,
    public notificationService: NotificationService
  ) {
    this.breadCrump = {
      Title: 'ثبت بنر جدید',
      DarkBackground: true,
      FirstNode: new UrlNamePair('داشبورد', '/controlpanel/dashboard'),
      SecondNode: new UrlNamePair('مدیریت بنرها - اسلاید ها', '/controlpanel/pages'),
      ThirdNode: new UrlNamePair('ثبت بنر جدید'),
      // Button: [
      //   {
      //     ButtonText: 'ثبت بنر',
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

  // save(form: any) {
  //   this.redirect = true;
  //   const model = form.value as SliderCreateModel;
  //   this.submitting = true;
  //   this.service.create(model).subscribe(
  //     (response) => {
  //       this.submitting = false;
  //       this.notificationService.successfulOperation('ثبت بنر جدید');
  //       if (this.redirect) {
  //         this.router.navigateByUrl('/controlpanel/pages');
  //       } else {
  //         this.form.reset();
  //       }
  //     },
  //     (error) => {
  //       this.submitting = false;
  //       this.notificationService.serverError();
  //       console.log(error);
  //     }
  //   );
  // }

  save() {
    this.submitting = true;

    this.sliderList.forEach(item => {
      item.groupName = this.groupName;
    });

    this.service.create(this.sliderList).subscribe(
      (response) => {
        this.notificationService.successfulOperation('ثبت بنر جدید');
        this.router.navigateByUrl('/controlpanel/sliders');
      },
      (error) => {
        this.submitting = false;
        this.notificationService.serverError();
        console.log(error);
      }
    );
  }

  onUploadSuccess($event) {
    $event[1].forEach(item => {
      const newFile = {
        fileName: item.fileName,
        fileId: item.id,
        description: '',
        imageAddress: Api.common.image + item.id,
        title: item.fileName
      } as SliderCreateModel;
      this.sliderList.push(newFile);
    });
  }

  onRemoveFile($event) {
    const index = this.sliderList.findIndex(x => x.fileName === $event.name);
    const currentRow = this.sliderList[index];
    this.fileService.deleteFile(currentRow.fileId);
    this.sliderList.splice(index, 1);
  }

  onUploadError($event) {
    this.notificationService.serverError();
  }
}
