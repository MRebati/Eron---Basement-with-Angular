import { NgModule } from '@angular/core';

import { BaseModule } from '../base/base.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { LaddaModule } from 'angular2-ladda';
import { PubSubModule } from 'angular2-pubsub';
import { ToastModule } from 'ng2-toastr/src/toast.module';

import { SideComponent } from './layout/side/side.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { DefaultComponent } from './default/default.component';
import { LoginComponent } from '../authentication/login/login.component';
import { RightSideComponent } from './layout/right-side/right-side.component';
import { SkinConfigComponent } from './layout/skin-config/skin-config.component';
import { ControlPanelComponent } from './control-panel.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { BreadCrumpComponent } from './layout/bread-crump/bread-crump.component';
import { InsightComponent } from './insight/insight.component';
import { SupportComponent } from './support/support.component';
import { PageComponent } from './base/page/page.component';
import { MenuComponent } from './Base/navigation/menu/menu.component';
import { SocialComponent } from './Base/Navigation/social/social.component';
import { LinkComponent } from './base/navigation/link/link.component';
import { BadgeComponent } from '../base/components/badge-component/badge-component.component';
import { ControlPanelRoutingModule } from './control-panel.routing';
import { ControlPanelSharedModule } from './control-panel.shared.module';
import { DropzoneModule, DROPZONE_CONFIG } from 'ngx-dropzone-wrapper/dist/lib/dropzone.module';
import { NavigationComponent } from './base/navigation/navigation.component';
import { PageCreateComponent } from './base/page/page-create/page-create.component';
import { PageUpdateComponent } from './base/page/page-update/page-update.component';
import { PageListComponent } from './base/page/page-list/page-list.component';
import { UserComponent } from './base/user/user.component';
import { UserCreateComponent } from './base/user/user-create/user-create.component';
import { UserListComponent } from './base/user/user-list/user-list.component';
import { UserUpdateComponent } from './base/user/user-update/user-update.component';
import { FooterCreateComponent } from './base/navigation/footer/footer-create/footer-create.component';
import { LinkItemComponent } from './base/navigation/link/link-item/link-item.component';
import { MenuCreateComponent } from './base/navigation/menu/menu-create/menu-create.component';
import { SliderComponent } from './base/slider/slider.component';
import { SliderListComponent } from './base/slider/slider-list/slider-list.component';
import { SliderCreateComponent } from './base/slider/slider-create/slider-create.component';
import { SliderUpdateComponent } from './base/slider/slider-update/slider-update.component';
import { UserDetailsComponent } from './base/user/user-details/user-details.component';
import { LinkDetailsComponent } from './base/navigation/link/link-details/link-details.component';
import { LinkUpdateComponent } from './base/navigation/link/link-update/link-update.component';

@NgModule({
  declarations: [
    SideComponent,
    HeaderComponent,
    FooterComponent,
    DefaultComponent,
    RightSideComponent,
    SkinConfigComponent,
    ControlPanelComponent,
    UserDashboardComponent,
    InsightComponent,
    SupportComponent,
    PageComponent,
    MenuComponent,
    SocialComponent,
    LinkComponent,
    NavigationComponent,
    PageCreateComponent,
    PageUpdateComponent,
    PageListComponent,
    UserComponent,
    UserCreateComponent,
    UserListComponent,
    UserUpdateComponent,
    FooterCreateComponent,
    LinkItemComponent,
    MenuCreateComponent,
    SliderComponent,
    SliderListComponent,
    SliderCreateComponent,
    SliderUpdateComponent,
    UserDetailsComponent,
    LinkDetailsComponent,
    LinkUpdateComponent
  ],
  imports: [
    BaseModule,
    ControlPanelSharedModule,
    ControlPanelRoutingModule
  ]
})
export class ControlPanelModule { }
