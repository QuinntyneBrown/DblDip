import { TestBed } from '@angular/core/testing';

import { CurrentProfileGuard } from './current-profile.guard';

describe('CurrentProfileGuard', () => {
  let guard: CurrentProfileGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(CurrentProfileGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
