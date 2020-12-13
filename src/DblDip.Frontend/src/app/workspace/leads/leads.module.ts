import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeadsComponent } from './leads/leads.component';



@NgModule({
  declarations: [LeadsComponent],
  exports: [LeadsComponent],
  imports: [
    CommonModule
  ]
})
export class LeadsModule { }
