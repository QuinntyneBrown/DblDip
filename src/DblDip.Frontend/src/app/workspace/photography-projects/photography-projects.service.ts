import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { PhotographyProject } from './photography-project';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PhotographyProjectsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<PhotographyProject[]> {
    return this._client.get<{ photographyProjects: PhotographyProject[] }>(`${this._baseUrl}api/photographyProjects`)
      .pipe(
        map(x => x.photographyProjects)
      );
  }

  public getById(options: { photographyProjectId: number }): Observable<PhotographyProject> {
    return this._client.get<{ photographyProject: PhotographyProject }>(`${this._baseUrl}api/photographyProjects/${options.photographyProjectId}`)
      .pipe(
        map(x => x.photographyProject)
      );
  }

  public remove(options: { photographyProject: PhotographyProject }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/photographyProjects/${options.photographyProject.photographyProjectId}`);
  }

  public save(options: { photographyProject: PhotographyProject }): Observable<{ photographyProjectId: number }> {
    return this._client.post<{ photographyProjectId: number }>(`${this._baseUrl}api/photographyProjects`, { photographyProject: options.photographyProject });
  }  
}
