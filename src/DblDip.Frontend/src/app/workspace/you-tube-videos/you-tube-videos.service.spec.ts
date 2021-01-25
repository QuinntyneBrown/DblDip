import { TestBed } from '@angular/core/testing';

import { YouTubeVideosService } from './you-tube-videos.service';

describe('YouTubeVideosService', () => {
  let service: YouTubeVideosService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YouTubeVideosService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
