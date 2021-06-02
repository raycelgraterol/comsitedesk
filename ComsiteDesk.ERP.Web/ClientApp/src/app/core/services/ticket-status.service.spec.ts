import { TestBed } from '@angular/core/testing';

import { TicketStatusService } from './ticket-status.service';

describe('TicketStatusService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TicketStatusService = TestBed.get(TicketStatusService);
    expect(service).toBeTruthy();
  });
});
