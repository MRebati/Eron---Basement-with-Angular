import { NgModule } from '@angular/core';
import { PubSubModule } from 'angular2-pubsub';
import { ToastModule } from 'ng2-toastr';
import { RecaptchaModule } from 'ng2-recaptcha';
import { AgmCoreModule } from '@agm/core';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { LaddaModule } from 'angular2-ladda';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { NgxPaginationModule } from 'ngx-pagination';
import { CKEditorModule } from 'ngx-ckeditor';
import { ModalModule } from 'ngx-modal';

@NgModule({
  imports: [
    PubSubModule.forRoot(),
    ToastModule.forRoot(),
    RecaptchaModule.forRoot(),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCs8nSEumLi8FjQrodnZEWl4H36P_UpTow'
    }),
    DropzoneModule.forChild(),
    Ng2SmartTableModule,
    LaddaModule,
    CurrencyMaskModule,
    NgxPaginationModule,
    CKEditorModule,
    ModalModule
  ],
  exports: [
    Ng2SmartTableModule,
    LaddaModule,
    CurrencyMaskModule,
    NgxPaginationModule,
    CKEditorModule,
    ModalModule,
    RecaptchaModule,
    ToastModule,
    PubSubModule,
    AgmCoreModule
  ]
})
export class ThirdPartyModule {
}

