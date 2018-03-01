import { AuthenticationService } from './auth.service';
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';

@Injectable()
export class AdminAuthGuard implements CanActivate {
  constructor(
    private authService: AuthenticationService
  ) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    const isAuthorized = this.authService.isAuthorized('admin');
    return isAuthorized;
  }

}
