import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YouTubeVideoEditorComponent } from './you-tube-video-editor.component';

describe('YouTubeVideoEditorComponent', () => {
  let component: YouTubeVideoEditorComponent;
  let fixture: ComponentFixture<YouTubeVideoEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YouTubeVideoEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(YouTubeVideoEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
