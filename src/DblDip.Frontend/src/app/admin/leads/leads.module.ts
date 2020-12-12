import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeadsRoutingModule } from './leads-routing.module';
import { LeadsComponent } from './leads/leads.component';
import { CoreModule } from '../../core/core.module';
import { SharedModule } from '../../shared/shared.module';
import { LeadsService } from './leads.service';


@NgModule({
  declarations: [LeadsComponent],
  imports: [
    CoreModule,
    SharedModule,
    CommonModule,
    LeadsRoutingModule
  ],
  providers:[
    LeadsService
  ]
})
export class LeadsModule { }
