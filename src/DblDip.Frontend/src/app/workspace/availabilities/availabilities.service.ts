import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Availability } from './availability';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AvailabilitiesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Availability[]> {
    return this._client.get<{ availabilities: Availability[] }>(`${this._baseUrl}api/availabilities`)
      .pipe(
        map(x => x.availabilities)
      );
  }

  public getById(options: { availabilityId: number }): Observable<Availability> {
    return this._client.get<{ availability: Availability }>(`${this._baseUrl}api/availabilities/${options.availabilityId}`)
      .pipe(
        map(x => x.availability)
      );
  }

  public remove(options: { availability: Availability }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/availabilities/${options.availability.availabilityId}`);
  }

  public save(options: { availability: Availability }): Observable<{ availabilityId: number }> {
    return this._client.post<{ availabilityId: number }>(`${this._baseUrl}api/availabilities`, { availability: options.availability });
  }  
}
