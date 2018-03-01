
import { Injectable } from '@angular/core';
// tslint:disable-next-line:import-blacklist
import * as rxjs from 'rxjs';
import { HttpClient } from './app.http.service';
import { Api } from '../api';
@Injectable()
export class SelectListService {
  http: HttpClient;
  constructor( http: HttpClient) {
    this.http = http;
  }

  getSelectList(name: string) {
    const url = Api.common.selectListService + name;
    const result = this.http.get(url);
    return result;
  }
}
