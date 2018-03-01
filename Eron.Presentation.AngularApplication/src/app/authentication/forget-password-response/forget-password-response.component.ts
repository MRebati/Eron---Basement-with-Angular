import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../auth.service';
import { NotificationService } from '../../base/services/notification.service';
import { ForgetPasswordResponseModel } from './forget-password-response.model';
import { LoginViewModel } from '../login.model';
import { Config } from '../../app.config';

@Component({
  selector: 'app-forget-password-response',
  templateUrl: './forget-password-response.component.html',
  styleUrls: ['./forget-password-response.component.scss']
})
export class ForgetPasswordResponseComponent implements OnInit {
  companyName = Config.application.companyName;
  currentYear = new Date();
  submitting: boolean;
  code: string;
  model: ForgetPasswordResponseModel = {} as ForgetPasswordResponseModel;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthenticationService,
    private notificationService: NotificationService
  ) {
    this.activatedRoute.params.subscribe(
      param => {
        this.code = param['code'];
        this.model.code = this.code;
      }
    );
  }

  ngOnInit() {
  }

  changePassword() {
    this.submitting = true;
    this.authService.resetPassword(this.model).subscribe(
      (response) => {
        this.notificationService.successfulOperationWithAlert('رمز عبور شما تغییر کرد', 'برای ورود به سامانه از رمز عبور جدید استفاده کنید');
        this.router.navigateByUrl('/login');
      },
      (error) => {
        this.submitting = false;
      }
    );
  }
}
