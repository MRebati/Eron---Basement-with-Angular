import { Component, OnInit } from '@angular/core';
import { Config } from '../../app.config';
import { AuthenticationService } from '../auth.service';
import { NotificationService } from '../../base/services/notification.service';

@Component({
  selector: 'app-forget-password-request',
  templateUrl: './forget-password-request.component.html',
  styleUrls: ['./forget-password-request.component.scss']
})
export class ForgetPasswordRequestComponent implements OnInit {

  currentYear = new Date();
  companyName = Config.application.companyName;
  emailAddress: string;
  submited: boolean;
  submitting: boolean;
  constructor(
    private authService: AuthenticationService,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
  }

  sendCode() {
    this.submitting = true;
    this.authService.forgetPassword(this.emailAddress).subscribe(
      (response) => {
        this.submitting = false;
        this.submited = true;
      },
      (error) => {
        this.submitting = false;
        this.submited = false;
        this.notificationService.serverError(error);
      }
    );
  }

}
