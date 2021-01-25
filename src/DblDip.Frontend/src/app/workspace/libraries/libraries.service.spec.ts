import { TestBed } from '@angular/core/testing';

import { LibrariesService } from './libraries.service';

describe('LibrariesService', () => {
  let service: LibrariesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LibrariesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
