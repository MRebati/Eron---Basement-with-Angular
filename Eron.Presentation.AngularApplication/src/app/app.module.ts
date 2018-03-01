import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler, Injector } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PubSubModule } from 'angular2-pubsub';
import { ToastModule } from 'ng2-toastr/ng2-toastr';

import { AppRoutingModule } from './app-routing.module';
import { SelectListService } from './base/services/selectList.service';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { LaddaModule } from 'angular2-ladda';
import { AppComponent } from './app.component';
import { AuthenticationService } from './authentication/auth.service';
import { HttpModule } from '@angular/http';
import { Api } from './base/api';
import { CustomToastOption } from './base/config/CustomToastOption';
import { SweetAlertService } from 'angular-sweetalert-service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastProvider } from './base/config/ToastProvider';
import { ControlPanelModule } from './control-panel/control-panel.module';
import { AuthModule } from './authentication/auth.module';
import { CommonService } from './base/services/common.service';
import { DropZoneConfiguration, DropZoneEronConfig } from './base/config/DropZoneConfiguration';
import { PageService } from './control-panel/base/page/page.service';
import { UserService } from './control-panel/base/user/user.service';
import { AuthGuard } from './authentication/auth.guard';
import { AdminAuthGuard } from './authentication/admin.auth.guard';
import { NavigationService } from './control-panel/base/navigation/navigation.service';
import { NotificationService } from './base/services/notification.service';
import { SliderService } from './control-panel/base/slider/slider.service';
import { ToastOptions } from 'ng2-toastr/src/toast-options';
import { DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { DefaultErrorHandler } from './base/exceptionHandlers/defaultExceptionHandler';
import { DropzoneModule } from 'ngx-dropzone-wrapper/dist/lib/dropzone.module';
import { ProvinceService } from './base/services/province.service';
import { RecaptchaLoaderService } from 'ng2-recaptcha/recaptcha/recaptcha-loader.service';
import { AgmCoreModule } from '@agm/core';
import { InsightsService } from './base/services/insights.service';
import { HttpClient } from './base/services/app.http.service';
import { FileService } from './base/services/file.service';
import { StorageService } from './base/services/storage.service';
import { BaseModule } from './base/base.module';
import { AppState } from './app.service';
import { GlobalErrorHandler } from './base';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AuthModule,
    DropzoneModule.forRoot(),
    BaseModule
  ],
  providers: [
    AuthenticationService,
    HttpClient,
    SelectListService,
    CommonService,
    FileService,
    PageService,
    SliderService,
    UserService,
    NavigationService,
    SweetAlertService,
    NotificationService,
    AuthGuard,
    AdminAuthGuard,
    ProvinceService,
    InsightsService,
    StorageService,
    {
      provide: ToastOptions,
      useClass: CustomToastOption
    },
    {
      provide: DROPZONE_CONFIG,
      useValue: DropZoneEronConfig()
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  static injector: Injector;
  constructor(public appState: AppState, injector: Injector) {
    AppModule.injector = injector;
  }
}
