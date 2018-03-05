import { NgModule, ErrorHandler, Injector } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { RecaptchaLoaderService } from 'ng2-recaptcha/recaptcha/recaptcha-loader.service';
import { AgmCoreModule } from '@agm/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule, AuthenticationService, AuthGuard, AdminAuthGuard } from './authentication';
import { DropzoneModule, DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { BaseModule } from './base/base.module';
import { SelectListService, CommonService, FileService, NotificationService, ProvinceService, StorageService, GlobalErrorHandler } from './base/services';
import { PageService } from './control-panel/base/page/page.service';
import { SliderService } from './control-panel/base/slider/slider.service';
import { UserService } from './control-panel/base/user/user.service';
import { NavigationService } from './control-panel/base/navigation/navigation.service';
import { SweetAlertService } from 'angular-sweetalert-service/js';
import { InsightsService } from './base/services/insights.service';
import { ToastOptions } from 'ng2-toastr';
import { CustomToastOption } from './base/config/CustomToastOption';
import { DropZoneEronConfig } from './base/config/DropZoneConfiguration';
import { AppState } from './app.service';

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
//  HttpClient,
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
    AppState,
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
