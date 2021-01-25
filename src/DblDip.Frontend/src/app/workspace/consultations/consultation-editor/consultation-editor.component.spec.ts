import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultationEditorComponent } from './consultation-editor.component';

describe('ConsultationEditorComponent', () => {
  let component: ConsultationEditorComponent;
  let fixture: ComponentFixture<ConsultationEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsultationEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultationEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
