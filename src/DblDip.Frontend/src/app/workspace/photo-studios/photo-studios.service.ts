import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { PhotoStudio } from './photo-studio';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PhotoStudiosService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<PhotoStudio[]> {
    return this._client.get<{ photoStudios: PhotoStudio[] }>(`${this._baseUrl}api/photoStudios`)
      .pipe(
        map(x => x.photoStudios)
      );
  }

  public getById(options: { photoStudioId: number }): Observable<PhotoStudio> {
    return this._client.get<{ photoStudio: PhotoStudio }>(`${this._baseUrl}api/photoStudios/${options.photoStudioId}`)
      .pipe(
        map(x => x.photoStudio)
      );
  }

  public remove(options: { photoStudio: PhotoStudio }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/photoStudios/${options.photoStudio.photoStudioId}`);
  }

  public save(options: { photoStudio: PhotoStudio }): Observable<{ photoStudioId: number }> {
    return this._client.post<{ photoStudioId: number }>(`${this._baseUrl}api/photoStudios`, { photoStudio: options.photoStudio });
  }  
}
