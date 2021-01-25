import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Point } from './point';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PointsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Point[]> {
    return this._client.get<{ points: Point[] }>(`${this._baseUrl}api/points`)
      .pipe(
        map(x => x.points)
      );
  }

  public getById(options: { pointId: number }): Observable<Point> {
    return this._client.get<{ point: Point }>(`${this._baseUrl}api/points/${options.pointId}`)
      .pipe(
        map(x => x.point)
      );
  }

  public remove(options: { point: Point }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/points/${options.point.pointId}`);
  }

  public save(options: { point: Point }): Observable<{ pointId: number }> {
    return this._client.post<{ pointId: number }>(`${this._baseUrl}api/points`, { point: options.point });
  }  
}
