import { Injectable } from '@angular/core';
import { HttpClient } from '../../../base/services/app.http.service';
import { Api } from '../../../base/api';
import { PageCreateModel } from './page-create/page-create.model';
import { ToastsManager } from 'ng2-toastr/src/toast-manager';
import { Router } from '@angular/router';
import { PageUpdateModel } from './page-update/page-update.model';

@Injectable()
export class PageService {

  constructor(
    private http: HttpClient,
    public toastr: ToastsManager,
    private router: Router) { }

  getAllPages() {
    return this.http.get(Api.page.default);
  }

  createPage(model: PageCreateModel) {
    return this.http.post(Api.page.default, model);
  }

  updatePage(model: PageUpdateModel) {
    return this.http.put(Api.page.default, model);
  }

  getPageBySlug(slug: string) {
    return this.http.get(Api.page.slug + slug);
  }

  getPageById(id: number) {
    return this.http.get(Api.page.default + id);
  }
}
