import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewsactionsComponent } from './viewsactions.component';

describe('ViewsactionsComponent', () => {
  let component: ViewsactionsComponent;
  let fixture: ComponentFixture<ViewsactionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewsactionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewsactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
