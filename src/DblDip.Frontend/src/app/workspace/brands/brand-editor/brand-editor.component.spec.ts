import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandEditorComponent } from './brand-editor.component';

describe('BrandEditorComponent', () => {
  let component: BrandEditorComponent;
  let fixture: ComponentFixture<BrandEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BrandEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BrandEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
