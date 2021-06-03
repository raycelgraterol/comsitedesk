import { TestBed } from '@angular/core/testing';

import { TicketTypesService } from './ticket-types.service';

describe('TicketTypesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TicketTypesService = TestBed.get(TicketTypesService);
    expect(service).toBeTruthy();
  });
});
