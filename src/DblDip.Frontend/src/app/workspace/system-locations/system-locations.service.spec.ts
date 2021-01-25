import { TestBed } from '@angular/core/testing';

import { SystemLocationsService } from './system-locations.service';

describe('SystemLocationsService', () => {
  let service: SystemLocationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SystemLocationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
