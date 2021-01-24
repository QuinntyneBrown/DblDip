import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardHeaderComponent } from './dashboard-header/dashboard-header.component';
import { ManageDashboardsComponent } from './manage-dashboards/manage-dashboards.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardCardConfigurationComponent } from './dashboard-card-configuration/dashboard-card-configuration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule } from '@shared';
import { CreateDashboardComponent } from './create-dashboard/create-dashboard.component';

const declarations = [DashboardHeaderComponent, ManageDashboardsComponent, DashboardComponent, DashboardCardConfigurationComponent, CreateDashboardComponent];

@NgModule({
  declarations,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    SharedModule
  ],
  exports: declarations
})
export class DashboardsModule { }
