import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WeeklyOrdersComponent } from './weekly-orders.component';

describe('WeeklyOrdersComponent', () => {
  let component: WeeklyOrdersComponent;
  let fixture: ComponentFixture<WeeklyOrdersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WeeklyOrdersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WeeklyOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
