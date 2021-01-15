import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService, RedirectService } from '@core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { DashboardsService } from '../dashboards/dashboards.service';

@Component({
  selector: 'app-workspace',
  templateUrl: './workspace.component.html',
  styleUrls: ['./workspace.component.scss']
})
export class WorkspaceComponent implements OnInit, OnDestroy {

  private readonly destroyed: Subject<void> = new Subject();

  public vm$: Observable<any> | undefined;

  constructor(
    private authService: AuthService, 
    private dashboardService: DashboardsService,
    private redirectService: RedirectService
    ) { }

  ngOnInit(): void {
    //this.dashboardService.getCurrentDashboardByProfileId()
  }

  public logout() {
    this.authService.logout();
    this.redirectService.redirectToLogin();
  }

  ngOnDestroy() {
    this.destroyed.complete();
    this.destroyed.next()
  }
}
