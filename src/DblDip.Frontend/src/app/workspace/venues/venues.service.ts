import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Venue } from './venue';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VenuesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Venue[]> {
    return this._client.get<{ venues: Venue[] }>(`${this._baseUrl}api/venues`)
      .pipe(
        map(x => x.venues)
      );
  }

  public getById(options: { venueId: number }): Observable<Venue> {
    return this._client.get<{ venue: Venue }>(`${this._baseUrl}api/venues/${options.venueId}`)
      .pipe(
        map(x => x.venue)
      );
  }

  public remove(options: { venue: Venue }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/venues/${options.venue.venueId}`);
  }

  public save(options: { venue: Venue }): Observable<{ venueId: number }> {
    return this._client.post<{ venueId: number }>(`${this._baseUrl}api/venues`, { venue: options.venue });
  }  
}
