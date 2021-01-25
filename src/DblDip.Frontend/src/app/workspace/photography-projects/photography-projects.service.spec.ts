import { TestBed } from '@angular/core/testing';

import { PhotographyProjectsService } from './photography-projects.service';

describe('PhotographyProjectsService', () => {
  let service: PhotographyProjectsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhotographyProjectsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
