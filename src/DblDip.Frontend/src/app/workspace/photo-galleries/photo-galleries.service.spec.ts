import { TestBed } from '@angular/core/testing';

import { PhotoGalleriesService } from './photo-galleries.service';

describe('PhotoGalleriesService', () => {
  let service: PhotoGalleriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhotoGalleriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
