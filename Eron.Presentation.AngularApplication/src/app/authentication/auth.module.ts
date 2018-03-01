import { NgModule } from '@angular/core';

import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AuthenticationService } from './auth.service';
import { HttpClient } from '../base/services/app.http.service';
import { BaseModule } from '../base/base.module';
import { ForgetPasswordRequestComponent } from './forget-password-request/forget-password-request.component';
import { ForgetPasswordResponseComponent } from './forget-password-response/forget-password-response.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ForgetPasswordRequestComponent,
    ForgetPasswordResponseComponent,
  ],
  imports: [
    BaseModule
  ]
})
export class AuthModule { }
