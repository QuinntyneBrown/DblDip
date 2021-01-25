import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { SystemLocation } from './system-location';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SystemLocationsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<SystemLocation[]> {
    return this._client.get<{ systemLocations: SystemLocation[] }>(`${this._baseUrl}api/systemLocations`)
      .pipe(
        map(x => x.systemLocations)
      );
  }

  public getById(options: { systemLocationId: number }): Observable<SystemLocation> {
    return this._client.get<{ systemLocation: SystemLocation }>(`${this._baseUrl}api/systemLocations/${options.systemLocationId}`)
      .pipe(
        map(x => x.systemLocation)
      );
  }

  public remove(options: { systemLocation: SystemLocation }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/systemLocations/${options.systemLocation.systemLocationId}`);
  }

  public save(options: { systemLocation: SystemLocation }): Observable<{ systemLocationId: number }> {
    return this._client.post<{ systemLocationId: number }>(`${this._baseUrl}api/systemLocations`, { systemLocation: options.systemLocation });
  }  
}
