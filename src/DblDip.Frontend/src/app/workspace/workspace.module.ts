import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkspaceComponent } from './workspace/workspace.component';
import { WorkspaceHeaderComponent } from './workspace-header/workspace-header.component';
import { LeadsModule } from './leads/leads.module';
import { RouterModule } from '@angular/router';
import { WorkspaceRoutingModule } from './workspace-routing.module';
import { ClientsModule } from './clients/clients.module';

@NgModule({
  declarations: [WorkspaceComponent, WorkspaceHeaderComponent],
  imports: [
    ClientsModule,
    LeadsModule,
    WorkspaceRoutingModule,
    CommonModule,
    RouterModule,
  ]
})
export class WorkspaceModule { }
