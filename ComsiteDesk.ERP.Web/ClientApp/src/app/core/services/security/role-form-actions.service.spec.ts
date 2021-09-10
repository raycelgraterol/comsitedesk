import { TestBed } from '@angular/core/testing';

import { RoleFormActionsService } from './role-form-actions.service';

describe('RoleFormActionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RoleFormActionsService = TestBed.get(RoleFormActionsService);
    expect(service).toBeTruthy();
  });
});
