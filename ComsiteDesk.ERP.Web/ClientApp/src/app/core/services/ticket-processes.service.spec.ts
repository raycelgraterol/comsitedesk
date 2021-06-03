import { TestBed } from '@angular/core/testing';

import { TicketProcessesService } from './ticket-processes.service';

describe('TicketProcessesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TicketProcessesService = TestBed.get(TicketProcessesService);
    expect(service).toBeTruthy();
  });
});
