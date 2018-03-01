import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { LoginViewModel } from './login.model';
import { RegisterViewModel } from './register.model';
import { HttpClient } from '../base/services/app.http.service';
import { Api } from '../base/api';
import { Observable } from 'rxjs/Observable';
import { PubSubService } from 'angular2-pubsub';
import { OnDestroy } from '@angular/core';
import { ForgetPasswordResponseModel } from './forget-password-response/forget-password-response.model';

@Injectable()
export class AuthenticationService {
  constructor(
    private http: HttpClient
  ) {
  }

  login(input: LoginViewModel) {
    // input.grant_type = 'password';
    const newData = 'userName=' +
    encodeURIComponent(input.UserName) +
    '&password=' +
    encodeURIComponent(input.Password) +
    '&grant_type=password&recaptcha=' + encodeURIComponent(input.CaptchaResponse);
    const modelAsParameter = '';
    const headers: Headers = new Headers(
      {
        'Content-Type': 'application/x-www-form-urlencoded'
      });

    const response = this.http.post(Api.auth.token, newData).catch(
      (e) => (Observable.throw(this.errorHandler(e))));
    return response;
  }

  logout() {
    localStorage.removeItem('accessToken');
    return true;
  }

  register(input: RegisterViewModel) {
    const response = this.http.postRaw(Api.auth.register, input).catch(
      (e) => (Observable.throw(this.errorHandler(e))));
    return response;
  }

  isAuthenticated() {
    return localStorage.getItem('accessToken') != null;
  }

  isAuthorized(roleName: string): Observable<boolean> {
    return this.http.get(Api.auth.isInRole + roleName).catch(
      (e) => (Observable.throw(this.errorHandler(e))));
  }

  errorHandler(e) {
    console.log(e);
  }

  forgetPassword(emailAddress: string) {
    const model = {
      email: emailAddress
    };

    return this.http.post(Api.auth.forgetPassword, model);
  }

  resetPassword(resetPasswordModel: ForgetPasswordResponseModel) {
    return this.http.post(Api.auth.resetPassword, resetPasswordModel);
  }
}
