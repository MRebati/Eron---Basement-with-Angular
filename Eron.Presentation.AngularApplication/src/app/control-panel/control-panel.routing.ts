import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ControlPanelComponent } from './control-panel.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { InsightComponent } from './insight/insight.component';
import { PageComponent } from './base/page/page.component';
import { LinkComponent } from './base/navigation/link/link.component';
import { SupportComponent } from './support/support.component';
import { DefaultComponent } from './default/default.component';
import { PageListComponent } from './base/page/page-list/page-list.component';
import { PageCreateComponent } from './base/page/page-create/page-create.component';
import { PageUpdateComponent } from './base/page/page-update/page-update.component';
import { UserComponent } from './base/user/user.component';
import { UserListComponent } from './base/user/user-list/user-list.component';
import { UserCreateComponent } from './base/user/user-create/user-create.component';
import { UserUpdateComponent } from './base/user/user-update/user-update.component';
import { AdminAuthGuard } from '../authentication/admin.auth.guard';
import { SliderComponent } from './base/slider/slider.component';
import { SliderListComponent } from './base/slider/slider-list/slider-list.component';
import { SliderCreateComponent } from './base/slider/slider-create/slider-create.component';
import { SliderUpdateComponent } from './base/slider/slider-update/slider-update.component';

const routes: Routes = [{
  path: '',
  component: ControlPanelComponent,
  canActivate: [AdminAuthGuard],
  // loadChildren: 'app/control-panel/control-panel.module#ControlPanelModule',
  children:
    [
      { path: 'dashboard', component: DefaultComponent },
      { path: 'insight', component: InsightComponent },
      {
        path: 'pages', component: PageComponent, children:
          [
            { path: '', component: PageListComponent },
            { path: 'create', component: PageCreateComponent },
            { path: 'update/:id', component: PageUpdateComponent }
          ]
      },
      {
        path: 'sliders', component: SliderComponent, children:
          [
            { path: 'create', component: SliderCreateComponent },
            { path: 'updte/:id', component: SliderUpdateComponent },
            { path: '', component: SliderListComponent },
          ]
      },
      { path: 'links', component: LinkComponent },
      {
        path: 'users', component: UserComponent, children:
          [
            { path: '', component: UserListComponent },
            { path: 'create', component: UserCreateComponent },
            { path: 'update/:id', component: UserUpdateComponent }
          ]
      },
      { path: 'support', component: SupportComponent },
      { path: '', component: DefaultComponent }
    ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ControlPanelRoutingModule {
  public ControlPanelRoutes = routes[0];
}
