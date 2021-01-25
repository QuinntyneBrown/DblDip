import { TestBed } from '@angular/core/testing';

import { QuestionnairesService } from './questionnaires.service';

describe('QuestionnairesService', () => {
  let service: QuestionnairesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuestionnairesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
