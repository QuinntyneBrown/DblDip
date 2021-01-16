import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardHeaderComponent } from './dashboard-header/dashboard-header.component';
import { ManageDashboardsComponent } from './manage-dashboards/manage-dashboards.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DashboardCardConfigurationComponent } from './dashboard-card-configuration/dashboard-card-configuration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const declarations = [DashboardHeaderComponent, ManageDashboardsComponent, DashboardComponent, DashboardCardConfigurationComponent];

@NgModule({
  declarations,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: declarations,
})
export class DashboardsModule { }
