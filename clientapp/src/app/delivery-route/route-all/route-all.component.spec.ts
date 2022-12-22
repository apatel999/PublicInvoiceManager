import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RouteAllComponent } from './route-all.component';

describe('RouteAllComponent', () => {
  let component: RouteAllComponent;
  let fixture: ComponentFixture<RouteAllComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RouteAllComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RouteAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
