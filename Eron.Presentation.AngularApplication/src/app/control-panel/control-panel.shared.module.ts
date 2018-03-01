import { NgModule } from '@angular/core';
import { BreadCrumpComponent } from './layout/bread-crump/bread-crump.component';
import { BadgeComponent } from '../base/components/badge-component/badge-component.component';
import { BaseModule } from '../base/base.module';

@NgModule({
  declarations: [
    BreadCrumpComponent
  ],
  imports: [
    BaseModule
  ],
  exports: [
    BreadCrumpComponent
  ]
})
export class ControlPanelSharedModule { }
