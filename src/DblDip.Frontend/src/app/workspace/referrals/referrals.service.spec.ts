import { TestBed } from '@angular/core/testing';

import { ReferralsService } from './referrals.service';

describe('ReferralsService', () => {
  let service: ReferralsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReferralsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
