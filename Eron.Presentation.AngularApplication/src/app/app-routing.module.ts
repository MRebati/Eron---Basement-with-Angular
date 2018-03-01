import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './authentication/login/login.component';
import { AppComponent } from './app.component';
import { RegisterComponent } from './authentication/register/register.component';
import { PreloadAllModules } from '@angular/router';
import { ControlPanelComponent } from './control-panel/control-panel.component';
import { AdminAuthGuard } from './authentication/admin.auth.guard';
import { ModalLoginComponent } from './authentication/modal/login/modal-login.component';
import { ForgetPasswordRequestComponent } from './authentication/forget-password-request/forget-password-request.component';
import { ForgetPasswordResponseComponent } from './authentication/forget-password-response/forget-password-response.component';

const appRoutes: Routes = [
  {
    path: '', component: AppComponent, children:
      [
        {
          path: 'controlpanel',
          canActivate: [AdminAuthGuard],
          loadChildren: 'app/control-panel/control-panel.module#ControlPanelModule',
        },
        { path: 'login', component: LoginComponent },
        { path: 'forgetpassword', component: ForgetPasswordRequestComponent },
        { path: 'forgetpassword/code/:code', component: ForgetPasswordResponseComponent },
        { path: 'register', component: RegisterComponent, },
//      { path: '', component: HomeComponent },
      ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})

export class AppRoutingModule {
}
