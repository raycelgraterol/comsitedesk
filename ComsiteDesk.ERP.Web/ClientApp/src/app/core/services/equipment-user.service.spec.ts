import { TestBed } from '@angular/core/testing';

import { EquipmentUserService } from './equipment-user.service';

describe('EquipmentUserService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EquipmentUserService = TestBed.get(EquipmentUserService);
    expect(service).toBeTruthy();
  });
});
