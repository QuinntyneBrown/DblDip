import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoGalleryEditorComponent } from './photo-gallery-editor.component';

describe('PhotoGalleryEditorComponent', () => {
  let component: PhotoGalleryEditorComponent;
  let fixture: ComponentFixture<PhotoGalleryEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhotoGalleryEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhotoGalleryEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
