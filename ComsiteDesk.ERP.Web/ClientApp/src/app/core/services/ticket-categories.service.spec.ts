import { TestBed } from '@angular/core/testing';

import { TicketCategoriesService } from './ticket-categories.service';

describe('TicketCategoriesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TicketCategoriesService = TestBed.get(TicketCategoriesService);
    expect(service).toBeTruthy();
  });
});
