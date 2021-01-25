import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotographyProjectEditorComponent } from './photography-project-editor.component';

describe('PhotographyProjectEditorComponent', () => {
  let component: PhotographyProjectEditorComponent;
  let fixture: ComponentFixture<PhotographyProjectEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhotographyProjectEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhotographyProjectEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
