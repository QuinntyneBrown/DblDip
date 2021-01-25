import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Ticket } from './ticket';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TicketsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Ticket[]> {
    return this._client.get<{ tickets: Ticket[] }>(`${this._baseUrl}api/tickets`)
      .pipe(
        map(x => x.tickets)
      );
  }

  public getById(options: { ticketId: number }): Observable<Ticket> {
    return this._client.get<{ ticket: Ticket }>(`${this._baseUrl}api/tickets/${options.ticketId}`)
      .pipe(
        map(x => x.ticket)
      );
  }

  public remove(options: { ticket: Ticket }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/tickets/${options.ticket.ticketId}`);
  }

  public save(options: { ticket: Ticket }): Observable<{ ticketId: number }> {
    return this._client.post<{ ticketId: number }>(`${this._baseUrl}api/tickets`, { ticket: options.ticket });
  }  
}
