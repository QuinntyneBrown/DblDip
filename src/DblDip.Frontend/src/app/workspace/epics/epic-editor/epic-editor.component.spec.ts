import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EpicEditorComponent } from './epic-editor.component';

describe('EpicEditorComponent', () => {
  let component: EpicEditorComponent;
  let fixture: ComponentFixture<EpicEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EpicEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EpicEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
