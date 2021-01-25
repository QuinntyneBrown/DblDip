import { TestBed } from '@angular/core/testing';

import { EpicsService } from './epics.service';

describe('EpicsService', () => {
  let service: EpicsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EpicsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
