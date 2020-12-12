import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { CoreModule } from '../../core/core.module';
import { SharedModule } from '../../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';


@NgModule({
  declarations: [HomeComponent],
  exports:[HomeComponent],
  imports: [
    CoreModule,
    SharedModule,
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
