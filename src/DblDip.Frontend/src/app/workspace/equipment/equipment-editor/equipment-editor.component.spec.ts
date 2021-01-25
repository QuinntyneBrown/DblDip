import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentEditorComponent } from './equipment-editor.component';

describe('EquipmentEditorComponent', () => {
  let component: EquipmentEditorComponent;
  let fixture: ComponentFixture<EquipmentEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EquipmentEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
