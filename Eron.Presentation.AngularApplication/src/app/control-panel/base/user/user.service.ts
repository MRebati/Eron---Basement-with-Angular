import { HttpClient } from '../../../base/services/app.http.service';
import { Api } from '../../../base/api';
import { Injectable } from '@angular/core';
import { ChangePasswordModel } from './change-password.model';

@Injectable()
export class UserService {
  /**
   *
   */
  constructor(
    private http: HttpClient
  ) { }

  getUsers() {
    return this.http.get(Api.user.default);
  }

  getRoles() {
    return this.http.get(Api.user.getRoles);
  }

  createUser(model: any) {
    return this.http.post(Api.user.default, model);
  }

  updateUser(model: any) {
    return this.http.put(Api.user.default, model);
  }

  updateUserAsAdmin(model: any) {
    return this.http.put(Api.user.updateAsAdmin, model);
  }

  deleteUser(id: number) {
    return this.http.delete(Api.user.default + id);
  }

  changePassword(model: ChangePasswordModel) {
    return this.http.post(Api.auth.changePassword, model);
  }

  userInfoIsComplete() {
    return this.http.get(Api.user.checkUserInfoIsComplete);
  }

  getUserInfo() {
    return this.http.get(Api.user.userInfo);
  }

  getUserFullInformationById(userId: string) {
    return this.http.get(Api.user.default + userId);
  }

  getUserRoles(userId: string) {
    return this.http.get(Api.user.userRoles + userId);
  }

}
