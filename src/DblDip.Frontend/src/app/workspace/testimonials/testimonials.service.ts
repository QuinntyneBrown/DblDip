import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Testimonial } from './testimonial';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TestimonialsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Testimonial[]> {
    return this._client.get<{ testimonials: Testimonial[] }>(`${this._baseUrl}api/testimonials`)
      .pipe(
        map(x => x.testimonials)
      );
  }

  public getById(options: { testimonialId: number }): Observable<Testimonial> {
    return this._client.get<{ testimonial: Testimonial }>(`${this._baseUrl}api/testimonials/${options.testimonialId}`)
      .pipe(
        map(x => x.testimonial)
      );
  }

  public remove(options: { testimonial: Testimonial }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/testimonials/${options.testimonial.testimonialId}`);
  }

  public save(options: { testimonial: Testimonial }): Observable<{ testimonialId: number }> {
    return this._client.post<{ testimonialId: number }>(`${this._baseUrl}api/testimonials`, { testimonial: options.testimonial });
  }  
}
