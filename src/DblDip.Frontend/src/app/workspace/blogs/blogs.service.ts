import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Blog } from './blog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BlogsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Blog[]> {
    return this._client.get<{ blogs: Blog[] }>(`${this._baseUrl}api/blogs`)
      .pipe(
        map(x => x.blogs)
      );
  }

  public getById(options: { blogId: number }): Observable<Blog> {
    return this._client.get<{ blog: Blog }>(`${this._baseUrl}api/blogs/${options.blogId}`)
      .pipe(
        map(x => x.blog)
      );
  }

  public remove(options: { blog: Blog }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/blogs/${options.blog.blogId}`);
  }

  public save(options: { blog: Blog }): Observable<{ blogId: number }> {
    return this._client.post<{ blogId: number }>(`${this._baseUrl}api/blogs`, { blog: options.blog });
  }  
}
