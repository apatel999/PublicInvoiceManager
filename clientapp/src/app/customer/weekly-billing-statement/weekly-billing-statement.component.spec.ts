import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WeeklyBillingStatementComponent } from './weekly-billing-statement.component';

describe('WeeklyBillingStatementComponent', () => {
  let component: WeeklyBillingStatementComponent;
  let fixture: ComponentFixture<WeeklyBillingStatementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WeeklyBillingStatementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WeeklyBillingStatementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
