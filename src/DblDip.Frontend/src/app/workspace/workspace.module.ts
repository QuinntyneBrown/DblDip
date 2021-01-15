import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkspaceComponent } from './workspace/workspace.component';
import { LeadsModule } from './leads/leads.module';
import { RouterModule } from '@angular/router';
import { WorkspaceRoutingModule } from './workspace-routing.module';
import { ClientsModule } from './clients/clients.module';
import { SharedModule } from '@shared/shared.module';
import { SelectProfileComponent } from './profiles/select-profile/select-profile.component';

@NgModule({
  declarations: [WorkspaceComponent, SelectProfileComponent],
  imports: [
    ClientsModule,
    LeadsModule,
    WorkspaceRoutingModule,
    CommonModule,
    RouterModule,
    SharedModule,
  ]
})
export class WorkspaceModule { }
