import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoStudioEditorComponent } from './photo-studio-editor.component';

describe('PhotoStudioEditorComponent', () => {
  let component: PhotoStudioEditorComponent;
  let fixture: ComponentFixture<PhotoStudioEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhotoStudioEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhotoStudioEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
