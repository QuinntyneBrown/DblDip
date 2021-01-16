import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Dashboard } from './dashboard';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DashboardsService {

  public currentDashboard$: BehaviorSubject<Dashboard> = new BehaviorSubject(null as any);
  
  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Dashboard[]> {
    return this._client.get<{ dashboards: Dashboard[] }>(`${this._baseUrl}api/dashboards`)
      .pipe(
        map(x => x.dashboards)
      );
  }

  public getCurrentDashboardsByCurrentProfile(): Observable<Dashboard[]> {
    return this._client.get<{ dashboards: Dashboard[] }>(`${this._baseUrl}api/dashboards/current-profile`)
      .pipe(
        map(x => x.dashboards)
      );
  }

  public getBydashboardId(options: { dashboardId: number }): Observable<Dashboard> {
    return this._client.get<{ dashboard: Dashboard }>(`${this._baseUrl}api/dashboards/${options.dashboardId}`)
      .pipe(
        map(x => x.dashboard)
      );
  }

  public remove(options: { dashboard: Dashboard }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/dashboards/${options.dashboard.dashboardId}`);
  }

  public save(options: { dashboard: Dashboard }): Observable<{ dashboardId: number }> {
    return this._client.post<{ dashboardId: number }>(`${this._baseUrl}api/dashboards`, { dashboard: options.dashboard });
  }  
}
