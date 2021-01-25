import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { PhotoGallery } from './photo-gallery';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PhotoGalleriesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<PhotoGallery[]> {
    return this._client.get<{ photoGalleries: PhotoGallery[] }>(`${this._baseUrl}api/photoGalleries`)
      .pipe(
        map(x => x.photoGalleries)
      );
  }

  public getById(options: { photoGalleryId: number }): Observable<PhotoGallery> {
    return this._client.get<{ photoGallery: PhotoGallery }>(`${this._baseUrl}api/photoGalleries/${options.photoGalleryId}`)
      .pipe(
        map(x => x.photoGallery)
      );
  }

  public remove(options: { photoGallery: PhotoGallery }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/photoGalleries/${options.photoGallery.photoGalleryId}`);
  }

  public save(options: { photoGallery: PhotoGallery }): Observable<{ photoGalleryId: number }> {
    return this._client.post<{ photoGalleryId: number }>(`${this._baseUrl}api/photoGalleries`, { photoGallery: options.photoGallery });
  }  
}
