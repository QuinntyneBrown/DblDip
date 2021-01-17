import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DialogService } from '@shared/dialog.service';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { switchMap, takeUntil, tap } from 'rxjs/operators';
import { Dashboard } from '../dashboard';
import { DashboardsService } from '../dashboards.service';
import { ManageDashboardsComponent } from '../manage-dashboards/manage-dashboards.component';

@Component({
  selector: 'app-dashboard-header',
  templateUrl: './dashboard-header.component.html',
  styleUrls: ['./dashboard-header.component.scss']
})
export class DashboardHeaderComponent implements OnInit, OnDestroy {

  private readonly _destroyed: Subject<void> = new Subject();

  public dashboards$: BehaviorSubject<Dashboard[]>  = new BehaviorSubject([] as Dashboard[]);
  
  public dashboardInEditMode: Partial<Dashboard> | null = null;

  public form = new FormGroup({ name: new FormControl('', [Validators.required]) });
  
  constructor(
    private _dashboardsService: DashboardsService,
    private _dialogService: DialogService
  ) { }

  ngOnInit(): void {
    this._dashboardsService.getCurrentDashboardsByCurrentProfile()
    .pipe(
      takeUntil(this._destroyed),
      tap(
        x => this.dashboards$.next(x)
      )
    ).subscribe();
  }

  tryToAddDashboard() { this.dashboardInEditMode = {}; }

  tryToSaveDashboard() {
    const dashboard = {
      name: this.form.value.name
    } as Dashboard;

    this._dashboardsService.save({ dashboard })
    .pipe(
      takeUntil(this._destroyed),
      tap(x => {
        this.dashboardInEditMode = null;
        this.form.reset();
      }),
      switchMap(x => this._dashboardsService.getCurrentDashboardsByCurrentProfile()),
      tap(x => this.dashboards$.next(x))
    )
    .subscribe();
  }

  manageDashboards() {
    this._dialogService.open<ManageDashboardsComponent>(ManageDashboardsComponent);
    
  }
  ngOnDestroy() {
    this._destroyed.next();
    this._destroyed.complete();
  }
}
