import { Injectable } from '@angular/core';
import { SliderCreateModel } from './slider-create/slider-create.model';
import { HttpClient } from '../../../base/services/app.http.service';
import { Api } from '../../../base/api';

@Injectable()
export class SliderService {
  constructor(private http: HttpClient) {

  }

  getList() {
    return this.http.get(Api.slider.default);
  }

  create(model: SliderCreateModel[]) {
    return this.http.post(Api.slider.default, model);
  }

  getByGroupName(groupName: string) {
    return this.http.get(Api.slider.default + groupName);
  }
}
