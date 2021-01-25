import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { TimeEntry } from './time-entry';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TimeEntriesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<TimeEntry[]> {
    return this._client.get<{ timeEntries: TimeEntry[] }>(`${this._baseUrl}api/timeEntries`)
      .pipe(
        map(x => x.timeEntries)
      );
  }

  public getById(options: { timeEntryId: number }): Observable<TimeEntry> {
    return this._client.get<{ timeEntry: TimeEntry }>(`${this._baseUrl}api/timeEntries/${options.timeEntryId}`)
      .pipe(
        map(x => x.timeEntry)
      );
  }

  public remove(options: { timeEntry: TimeEntry }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/timeEntries/${options.timeEntry.timeEntryId}`);
  }

  public save(options: { timeEntry: TimeEntry }): Observable<{ timeEntryId: number }> {
    return this._client.post<{ timeEntryId: number }>(`${this._baseUrl}api/timeEntries`, { timeEntry: options.timeEntry });
  }  
}
