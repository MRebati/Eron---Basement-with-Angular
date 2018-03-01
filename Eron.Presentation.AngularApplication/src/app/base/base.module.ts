import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SummernoteDirective, ICheckDirective, ICheckWithModelDirective, TagsInputDirective, NestableDirective, TagsInputWithModelDirective } from './directives';
import { IsParentPipe, PriceRialPipe, PriceTomanPipe, JalaliPipe, DefaultFilterPipe, LikeFilterPipe, SlugPipe } from './pipes';
import { ModalLoginComponent } from '../authentication/components';
import { HttpModule } from '@angular/http';
import { DropzoneModule, DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { LaddaModule } from 'angular2-ladda';
import { CurrencyMaskModule } from '../../assets/js/plugins/ng2-currency-mask';
import { ModalModule } from 'ngx-modal';
import { PubSubModule } from 'angular2-pubsub';
import { ToastModule } from 'ng2-toastr';
import { AgmCoreModule } from '@agm/core';
import { CommonModule } from '@angular/common';
import { DropZoneEronConfig } from './config/DropZoneConfiguration';
import {NgxPaginationModule} from 'ngx-pagination';
import { RecaptchaModule } from 'ng2-recaptcha';
import { CKEditorModule } from 'ngx-ckeditor';
import { ThirdPartyModule } from './thirdparty.module';
import { BadgeComponent } from './components/badge-component/badge-component.component';
import { CheckboxComponent } from './components/checkbox/checkbox.component';
import { RadioComponent } from './components/radio/radio.component';

@NgModule({
  declarations: [
    SummernoteDirective,
    ICheckDirective,
    ICheckWithModelDirective,
    TagsInputDirective,
    NestableDirective,
    TagsInputWithModelDirective,
    IsParentPipe,
    PriceRialPipe,
    PriceTomanPipe,
    JalaliPipe,
    DefaultFilterPipe,
    LikeFilterPipe,
    SlugPipe,
    BadgeComponent,
    CheckboxComponent,
    RadioComponent,
    ModalLoginComponent,

  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    HttpModule,
    ReactiveFormsModule,
    ThirdPartyModule
  ],
  exports: [
    CommonModule,
    RouterModule,
    FormsModule,
    HttpModule,
    ReactiveFormsModule,
    ThirdPartyModule,
    SummernoteDirective,
    ICheckDirective,
    ICheckWithModelDirective,
    TagsInputDirective,
    TagsInputWithModelDirective,
    BadgeComponent,
    CheckboxComponent,
    ModalLoginComponent,
    RadioComponent,
    NestableDirective,
    IsParentPipe,
    PriceRialPipe,
    PriceTomanPipe,
    JalaliPipe,
    DefaultFilterPipe,
    LikeFilterPipe,
    SlugPipe
  ],
  providers: [
    {
      provide: DROPZONE_CONFIG,
      useValue: DropZoneEronConfig()
    }
  ]
})
export class BaseModule { }
