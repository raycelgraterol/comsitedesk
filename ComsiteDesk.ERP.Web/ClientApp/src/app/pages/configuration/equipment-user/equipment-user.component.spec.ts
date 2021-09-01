import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentUserComponent } from './equipment-user.component';

describe('EquipmentUserComponent', () => {
  let component: EquipmentUserComponent;
  let fixture: ComponentFixture<EquipmentUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
