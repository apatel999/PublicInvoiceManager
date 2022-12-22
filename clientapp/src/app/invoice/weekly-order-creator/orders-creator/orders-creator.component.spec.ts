import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersCreatorComponent } from './orders-creator.component';

describe('OrdersCreatorComponent', () => {
  let component: OrdersCreatorComponent;
  let fixture: ComponentFixture<OrdersCreatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrdersCreatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
