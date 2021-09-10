import { TestBed } from '@angular/core/testing';

import { SecurityModulesService } from './security-modules.service';

describe('SecurityModulesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SecurityModulesService = TestBed.get(SecurityModulesService);
    expect(service).toBeTruthy();
  });
});
