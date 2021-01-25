import { TestBed } from '@angular/core/testing';

import { PaymentSchedulesService } from './payment-schedules.service';

describe('PaymentSchedulesService', () => {
  let service: PaymentSchedulesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PaymentSchedulesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
