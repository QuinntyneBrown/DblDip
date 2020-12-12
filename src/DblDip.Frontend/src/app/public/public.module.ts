import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { PublicRoutingModule } from './public-routing.module';
import { PublicComponent } from './public.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    PublicComponent
  ],
  providers: [

  ],
  imports: [
    CoreModule,
    SharedModule,
    CommonModule,
    PublicRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class PublicModule { }
