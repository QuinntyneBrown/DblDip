import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Service } from './service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ServicesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Service[]> {
    return this._client.get<{ services: Service[] }>(`${this._baseUrl}api/services`)
      .pipe(
        map(x => x.services)
      );
  }

  public getById(options: { serviceId: number }): Observable<Service> {
    return this._client.get<{ service: Service }>(`${this._baseUrl}api/services/${options.serviceId}`)
      .pipe(
        map(x => x.service)
      );
  }

  public remove(options: { service: Service }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/services/${options.service.serviceId}`);
  }

  public save(options: { service: Service }): Observable<{ serviceId: number }> {
    return this._client.post<{ serviceId: number }>(`${this._baseUrl}api/services`, { service: options.service });
  }  
}
