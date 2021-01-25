import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Brand } from './brand';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BrandsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Brand[]> {
    return this._client.get<{ brands: Brand[] }>(`${this._baseUrl}api/brands`)
      .pipe(
        map(x => x.brands)
      );
  }

  public getById(options: { brandId: number }): Observable<Brand> {
    return this._client.get<{ brand: Brand }>(`${this._baseUrl}api/brands/${options.brandId}`)
      .pipe(
        map(x => x.brand)
      );
  }

  public remove(options: { brand: Brand }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/brands/${options.brand.brandId}`);
  }

  public save(options: { brand: Brand }): Observable<{ brandId: number }> {
    return this._client.post<{ brandId: number }>(`${this._baseUrl}api/brands`, { brand: options.brand });
  }  
}
