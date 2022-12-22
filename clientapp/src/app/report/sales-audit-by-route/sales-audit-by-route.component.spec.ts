import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesAuditByRouteComponent } from './sales-audit-by-route.component';

describe('SalesAuditComponent', () => {
  let component: SalesAuditByRouteComponent;
  let fixture: ComponentFixture<SalesAuditByRouteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalesAuditByRouteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesAuditByRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
