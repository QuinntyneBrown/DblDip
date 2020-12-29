import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeadsComponent } from './leads/leads.component';
import { SharedModule } from '@shared';

@NgModule({
  declarations: [LeadsComponent],
  exports: [LeadsComponent],
  imports: [
    SharedModule,
    
    CommonModule
  ]
})
export class LeadsModule { }
