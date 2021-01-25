import { TestBed } from '@angular/core/testing';

import { ShotListsService } from './shot-lists.service';

describe('ShotListsService', () => {
  let service: ShotListsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShotListsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
