import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketProcessesComponent } from './ticket-processes.component';

describe('TicketProcessesComponent', () => {
  let component: TicketProcessesComponent;
  let fixture: ComponentFixture<TicketProcessesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketProcessesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketProcessesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
