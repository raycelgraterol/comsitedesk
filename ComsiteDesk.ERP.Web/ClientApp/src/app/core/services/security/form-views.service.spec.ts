import { TestBed } from '@angular/core/testing';

import { FormViewsService } from './form-views.service';

describe('FormViewsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormViewsService = TestBed.get(FormViewsService);
    expect(service).toBeTruthy();
  });
});
