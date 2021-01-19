import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService, RedirectService } from '@core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { takeUntil, tap } from 'rxjs/operators';
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
    private _authService: AuthService, 
    private _dashboardsService: DashboardsService,
    private _redirectService: RedirectService
    ) { }

  ngOnInit(): void {
    this._dashboardsService.getCurrentDashboardsByCurrentProfile()
    .pipe(
      takeUntil(this.destroyed),
      tap(x => this._dashboardsService.dashboards$.next(x))
    )
    .subscribe();
  }

  public logout() {
    this._authService.logout();
    this._redirectService.redirectToLogin();
  }

  ngOnDestroy() {
    this.destroyed.complete();
    this.destroyed.next()
  }
}
