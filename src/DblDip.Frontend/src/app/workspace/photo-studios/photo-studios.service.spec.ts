import { TestBed } from '@angular/core/testing';

import { PhotoStudiosService } from './photo-studios.service';

describe('PhotoStudiosService', () => {
  let service: PhotoStudiosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhotoStudiosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
