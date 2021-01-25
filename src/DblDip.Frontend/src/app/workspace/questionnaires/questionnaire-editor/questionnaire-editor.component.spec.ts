import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionnaireEditorComponent } from './questionnaire-editor.component';

describe('QuestionnaireEditorComponent', () => {
  let component: QuestionnaireEditorComponent;
  let fixture: ComponentFixture<QuestionnaireEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionnaireEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionnaireEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
