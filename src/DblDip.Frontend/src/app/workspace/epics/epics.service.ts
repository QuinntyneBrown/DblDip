import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Epic } from './epic';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EpicsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Epic[]> {
    return this._client.get<{ epics: Epic[] }>(`${this._baseUrl}api/epics`)
      .pipe(
        map(x => x.epics)
      );
  }

  public getById(options: { epicId: number }): Observable<Epic> {
    return this._client.get<{ epic: Epic }>(`${this._baseUrl}api/epics/${options.epicId}`)
      .pipe(
        map(x => x.epic)
      );
  }

  public remove(options: { epic: Epic }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/epics/${options.epic.epicId}`);
  }

  public save(options: { epic: Epic }): Observable<{ epicId: number }> {
    return this._client.post<{ epicId: number }>(`${this._baseUrl}api/epics`, { epic: options.epic });
  }  
}
