import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RolFormActionsComponent } from './rol-form-actions.component';

describe('RolFormActionsComponent', () => {
  let component: RolFormActionsComponent;
  let fixture: ComponentFixture<RolFormActionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RolFormActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RolFormActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
