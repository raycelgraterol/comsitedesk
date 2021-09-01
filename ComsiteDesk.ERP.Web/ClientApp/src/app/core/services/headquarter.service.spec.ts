import { TestBed } from '@angular/core/testing';

import { HeadquarterService } from './headquarter.service';

describe('HeadquarterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HeadquarterService = TestBed.get(HeadquarterService);
    expect(service).toBeTruthy();
  });
});
