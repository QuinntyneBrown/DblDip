import { TestBed } from '@angular/core/testing';

import { AvailabilitiesService } from './availabilities.service';

describe('AvailabilitiesService', () => {
  let service: AvailabilitiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AvailabilitiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
