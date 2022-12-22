import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BillingInfoFormComponent } from './billing-info-form.component';

describe('BillingInfoFormComponent', () => {
  let component: BillingInfoFormComponent;
  let fixture: ComponentFixture<BillingInfoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BillingInfoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BillingInfoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
