import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Company } from './company';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Company[]> {
    return this._client.get<{ companies: Company[] }>(`${this._baseUrl}api/companies`)
      .pipe(
        map(x => x.companies)
      );
  }

  public getById(options: { companyId: number }): Observable<Company> {
    return this._client.get<{ company: Company }>(`${this._baseUrl}api/companies/${options.companyId}`)
      .pipe(
        map(x => x.company)
      );
  }

  public remove(options: { company: Company }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/companies/${options.company.companyId}`);
  }

  public save(options: { company: Company }): Observable<{ companyId: number }> {
    return this._client.post<{ companyId: number }>(`${this._baseUrl}api/companies`, { company: options.company });
  }  
}
