import { TestBed } from '@angular/core/testing';

import { ConsultationsService } from './consultations.service';

describe('ConsultationsService', () => {
  let service: ConsultationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConsultationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
