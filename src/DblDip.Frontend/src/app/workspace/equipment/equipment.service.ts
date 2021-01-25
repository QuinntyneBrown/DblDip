import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Equipment } from './equipment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Equipment[]> {
    return this._client.get<{ equipment: Equipment[] }>(`${this._baseUrl}api/equipment`)
      .pipe(
        map(x => x.equipment)
      );
  }

  public getById(options: { equipmentId: number }): Observable<Equipment> {
    return this._client.get<{ equipment: Equipment }>(`${this._baseUrl}api/equipment/${options.equipmentId}`)
      .pipe(
        map(x => x.equipment)
      );
  }

  public remove(options: { equipment: Equipment }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/equipment/${options.equipment.equipmentId}`);
  }

  public save(options: { equipment: Equipment }): Observable<{ equipmentId: number }> {
    return this._client.post<{ equipmentId: number }>(`${this._baseUrl}api/equipment`, { equipment: options.equipment });
  }  
}
