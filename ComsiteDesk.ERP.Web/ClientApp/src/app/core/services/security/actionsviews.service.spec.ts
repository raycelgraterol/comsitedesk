import { TestBed } from '@angular/core/testing';

import { ActionsviewsService } from './actionsviews.service';

describe('ActionsviewsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ActionsviewsService = TestBed.get(ActionsviewsService);
    expect(service).toBeTruthy();
  });
});
