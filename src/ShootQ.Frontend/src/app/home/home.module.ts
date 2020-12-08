import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { CoreModule } from '../_core/core.module';
import { SharedModule } from '../_shared/shared.module';


@NgModule({
  declarations: [HomeComponent],
  exports:[HomeComponent],
  imports: [
    CoreModule,
    SharedModule,
    CommonModule
  ]
})
export class HomeModule { }
