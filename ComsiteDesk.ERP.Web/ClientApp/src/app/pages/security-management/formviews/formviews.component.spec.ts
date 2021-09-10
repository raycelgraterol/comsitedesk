import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormviewsComponent } from './formviews.component';

describe('FormviewsComponent', () => {
  let component: FormviewsComponent;
  let fixture: ComponentFixture<FormviewsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormviewsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormviewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
