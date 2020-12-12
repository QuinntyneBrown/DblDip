import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '../../core/constants';
import { HttpClient } from '@angular/common/http';
import { Lead } from './lead';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

alert("?");

@Injectable({
  providedIn: 'root'
})
export class LeadsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Lead[]> {
    return this._client.get<{ leads: Lead[] }>(`${this._baseUrl}api/leads`)
      .pipe(
        map(x => x.leads)
      );
  }

  public getById(options: { leadId: string }): Observable<Lead> {
    return this._client.get<{ lead: Lead }>(`${this._baseUrl}api/leads/${options.leadId}`)
      .pipe(
        map(x => x.lead)
      );
  }

  public remove(options: { lead: Lead }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/leads/${options.lead.leadId}`);
  }

  public save(options: { lead: Lead }): Observable<{ id: number }> {
    return this._client.post<{ id: number }>(`${this._baseUrl}api/leads`, { lead: options.lead });
  }  
}
