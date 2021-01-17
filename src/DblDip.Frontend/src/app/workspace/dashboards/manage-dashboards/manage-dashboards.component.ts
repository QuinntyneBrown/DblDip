import { OverlayRef } from '@angular/cdk/overlay';
import { Component, OnInit } from '@angular/core';
import { MatListOption } from '@angular/material/list';
import { Observable } from 'rxjs';
import { Dashboard } from '../dashboard';
import { DashboardsModule } from '../dashboards.module';
import { DashboardsService } from '../dashboards.service';

@Component({
  selector: 'app-manage-dashboards',
  templateUrl: './manage-dashboards.component.html',
  styleUrls: ['./manage-dashboards.component.scss']
})
export class ManageDashboardsComponent implements OnInit {

  public dashboards$: Observable<Dashboard[]> = this._dashboardsService.getCurrentDashboardsByCurrentProfile();

  constructor(private _dashboardsService: DashboardsService, private _overlayRef: OverlayRef) { }

  ngOnInit(): void {
  }

  close() {
    this._overlayRef.dispose();
  }

  remove(dashboards: MatListOption[]) {
    const dashboardIds = dashboards.map(x => `dashboardIds=${x.value.dashboardId}`).join('&');

    this._dashboardsService.removeDashboards({ dashboardIds })
    .subscribe();
    
  }
}
