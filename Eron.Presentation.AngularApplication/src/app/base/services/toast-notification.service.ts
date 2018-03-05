import { Injectable, OnInit } from '@angular/core';
import { SnotifyPosition, SnotifyService, SnotifyToast } from 'ng-snotify';
import { Observable } from 'rxjs/Observable';

import { ToastNotificationInterface } from './toast-notification.interface';

@Injectable()
export class TostNotificationService implements OnInit {

  toastConfig: ToastNotificationInterface;
  confirmBox: Observable<any>;
  constructor(public snotifyService: SnotifyService) {
    this.toastConfig = {
      title: 'Snotify title!',
      body: 'Lorem ipsum dolor sit amet!',
      timeout: 3000,
      position: SnotifyPosition.rightBottom,
      progressBar: true,
      closeClick: true,
      newTop: true,
      backdrop: -1,
      dockMax: 6,
      pauseHover: true,
      maxHeight: 300,
      titleMaxLength: 15,
      bodyMaxLength: 80,
    };
  }

  ngOnInit() {
    this.snotifyService.setDefaults({
      toast: {
        timeout: 3000,
        titleMaxLength: 14,
        bodyMaxLength: 40,
      },
      global: {
        newOnTop: false,
        maxOnScreen: 6
      }
    });
  }

  setGlobal() {
    this.snotifyService.setDefaults({
      toast: {
        bodyMaxLength: this.toastConfig.bodyMaxLength,
        titleMaxLength: this.toastConfig.titleMaxLength,
        backdrop: this.toastConfig.backdrop,
      },
      global: {
        newOnTop: this.toastConfig.newTop,
        maxOnScreen: this.toastConfig.dockMax,
      }
    });
  }

  onSuccess() {
    this.setGlobal();
    this.snotifyService.success(this.toastConfig.title, this.toastConfig.body, {
      timeout: this.toastConfig.timeout,
      showProgressBar: this.toastConfig.progressBar,
      closeOnClick: this.toastConfig.closeClick,
      pauseOnHover: this.toastConfig.pauseHover,
    });
  }
  onInfo() {
    this.setGlobal();
    this.snotifyService.info(this.toastConfig.title, this.toastConfig.body, {
      timeout: this.toastConfig.timeout,
      showProgressBar: this.toastConfig.progressBar,
      closeOnClick: this.toastConfig.closeClick,
      pauseOnHover: this.toastConfig.pauseHover,
    });
  }
  onError() {
    this.setGlobal();
    this.snotifyService.error(this.toastConfig.title, this.toastConfig.body, {
      timeout: this.toastConfig.timeout,
      showProgressBar: this.toastConfig.progressBar,
      closeOnClick: this.toastConfig.closeClick,
      pauseOnHover: this.toastConfig.pauseHover,
    });
  }
  onWarning() {
    this.setGlobal();
    this.snotifyService.warning(this.toastConfig.title, this.toastConfig.body, {
      timeout: this.toastConfig.timeout,
      showProgressBar: this.toastConfig.progressBar,
      closeOnClick: this.toastConfig.closeClick,
      pauseOnHover: this.toastConfig.pauseHover,
    });
  }
  onSimple() {
    this.setGlobal();

    // const icon = `assets/custom-svg.svg`;
    const icon = `https://placehold.it/48x100`;

    this.snotifyService.simple(this.toastConfig.title, this.toastConfig.body, {
      timeout: this.toastConfig.timeout,
      showProgressBar: this.toastConfig.progressBar,
      closeOnClick: this.toastConfig.closeClick,
      pauseOnHover: this.toastConfig.pauseHover,
      icon,
    });
  }

  onAsyncLoading(observable: Observable<any>) {
    this.setGlobal();
    const toast = this.snotifyService.async(this.toastConfig.title, this.toastConfig.body,
      /*
      You should pass Promise or Observable of type SnotifyConfig to change some data or do some other actions
      More information how to work with observables:
      https://github.com/Reactive-Extensions/RxJS/blob/master/doc/api/core/operators/create.md
       */

      // new Promise((resolve, reject) => {
      //   setTimeout(() => reject(), 1000);
      //   setTimeout(() => resolve(), 1500);
      // })
      Observable.create(observer => {
        observer.next({
          body: 'Processing...',
        });
        observable.subscribe(
          (success: any) => observer.complete(),
          (error: any) => observer.complete(),
          () => {
            setTimeout(() => {
              observer.complete(); this.snotifyService.remove(toast.id);
            }, 1000);
          });
      },
      ),
    );
  }

  onConfirmation() {


    this.confirmBox = new Observable(observer => {
      this.setGlobal();
      /*
         Here we pass an buttons array, which contains of 2 element of type SnotifyButton
          */
      const toast = this.snotifyService.confirm(this.toastConfig.title, this.toastConfig.body, {
        timeout: this.toastConfig.timeout,
        showProgressBar: this.toastConfig.progressBar,
        closeOnClick: this.toastConfig.closeClick,
        pauseOnHover: this.toastConfig.pauseHover,
        buttons: [
          // tslint:disable-next-line:no-console
          { text: 'Yes', action: () => { observer.next(); this.snotifyService.remove(toast.id); }, bold: false },
          // tslint:disable-next-line:max-line-length
          // tslint:disable-next-line:no-console
          { text: 'No', action: () => { observer.error(); this.snotifyService.remove(toast.id); }, bold: true },
        ],
      });
    });



  }

  onPrompt() {
    this.setGlobal();
    /*
     Here we pass an buttons array, which contains of 2 element of type SnotifyButton
     At the action of the first button we can get what user entered into input field.
     At the second we can't get it. But we can remove this toast
     */
    const toast = this.snotifyService.prompt(this.toastConfig.title, this.toastConfig.body, {
      timeout: this.toastConfig.timeout,
      showProgressBar: this.toastConfig.progressBar,
      closeOnClick: this.toastConfig.closeClick,
      pauseOnHover: this.toastConfig.pauseHover,
      buttons: [
        // tslint:disable-next-line:no-console
        { text: 'Yes', action: (text) => console.log(`Said Yes: ${text}`) },
        // tslint:disable-next-line:no-console
        { text: 'No', action: (text) => { console.log(`Said No: ${text}`); this.snotifyService.remove(toast.id); } },
      ],
      placeholder: 'This is the example placeholder which you can pass', // Max-length = 40
    });
  }


  onClear() {
    this.snotifyService.clear();
  }


}
