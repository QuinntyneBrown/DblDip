import { TestBed } from '@angular/core/testing';

import { TimeEntriesService } from './time-entries.service';

describe('TimeEntriesService', () => {
  let service: TimeEntriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TimeEntriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
