import { isDevMode } from '@angular/core';
import { environment } from '../environments/environment';

export class Config {
  static application = {
    name: 'ارون',
    description: 'سامانه مدیریت محتوا',
    mailAddress: 'info@eron.ir',
    phoneNumber: '0938-1616622',
    companyName: 'سامانه مدیریت محتوا وب ارون',
    copyright: 'تمام حقوق محفوظ می باشد',
    address: 'کرج، فردیس',
    hero: 'Er',
    social: {
      facebook: {
        address: '',
        name: ''
      },
      twitter: {
        address: '',
        name: ''
      },
      linkedin: {
        address: '',
        name: ''
      },
      googlePlus: {
        address: '',
        name: ''
      },
      instagram: {
        address: '',
        name: ''
      },
      pintrest: {
        address: '',
        name: ''
      },
    }
  };
}
