import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../auth.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { RegisterViewModel } from '../register.model';
import { LoginViewModel } from '../login.model';
import { Config } from '../../app.config';
import { NotificationService } from '../../base/services/notification.service';
import { RecaptchaComponent } from 'ng2-recaptcha/recaptcha/recaptcha.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  applicationName = Config.application.name;
  applicationDescription = Config.application.description;
  applicationHero = Config.application.hero;
  currentYear = new Date().getFullYear();
  form: FormGroup;
  submitting: boolean;
  termsAccepted: boolean;
  captchaResponse: string;
  @ViewChild('recaptcha') recaptcha: RecaptchaComponent;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private notificationService: NotificationService
  ) {
    this.form = fb.group({
      'email': ['', Validators.compose([Validators.required, Validators.minLength(5), Validators.email])],
      'password': ['', Validators.required],
      'confirmPassword': ['', Validators.required],
    });
  }

  ngOnInit() {
  }

  onSubmit(form: FormGroup) {
    this.submitting = true;
    const registerModel = this.form.value as RegisterViewModel;
    registerModel.recaptcha = this.captchaResponse;

    this.authService.register(registerModel).subscribe((Response) => {

      const loginModel: LoginViewModel = {
        UserName: registerModel.email,
        grant_type: 'password',
        CaptchaResponse: this.captchaResponse,
        Password: registerModel.password
      } as LoginViewModel;

      this.authService.login(loginModel).subscribe(
        (responseData) => {
          this.submitting = false;
          const loginResponse = responseData;
          localStorage.setItem('userName', loginResponse.userName);
          localStorage.setItem('accessToken', loginResponse.access_token);
          localStorage.setItem('refreshToken', loginResponse.refresh_token);
          this.router.navigateByUrl('/');
        },
        (error) => {
          this.submitting = false;
          this.recaptcha.reset();
          this.notificationService.serverError(error);
        }
      );
      // this.router.navigateByUrl('/controlpanel/dashboard');
    },
      (error) => {
        this.submitting = false;
        this.recaptcha.reset();
        this.notificationService.serverError(error);
      });
  }

  resolved(captchaResponse: string) {
    this.captchaResponse = captchaResponse;
  }

}
