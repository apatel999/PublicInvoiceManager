import { TestBed, inject } from '@angular/core/testing';

import { WeeklyOrdersService } from './weekly-orders.service';

describe('WeeklyOrdersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WeeklyOrdersService]
    });
  });

  it('should be created', inject([WeeklyOrdersService], (service: WeeklyOrdersService) => {
    expect(service).toBeTruthy();
  }));
});
