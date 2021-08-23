import { TestBed } from '@angular/core/testing';

import { AssignmentStatusService } from './assignment-status.service';

describe('AssignmentStatusService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AssignmentStatusService = TestBed.get(AssignmentStatusService);
    expect(service).toBeTruthy();
  });
});
