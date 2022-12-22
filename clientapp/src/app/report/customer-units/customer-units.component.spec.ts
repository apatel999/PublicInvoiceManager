import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerUnitsComponent } from './customer-units.component';

describe('CustomerUnitsComponent', () => {
  let component: CustomerUnitsComponent;
  let fixture: ComponentFixture<CustomerUnitsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerUnitsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerUnitsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
