import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientsComponent } from './clients/clients.component';
import { SharedModule } from '@shared';

@NgModule({
  declarations: [ClientsComponent],
  imports: [
    SharedModule,
    CommonModule
  ]
})
export class ClientsModule { }
