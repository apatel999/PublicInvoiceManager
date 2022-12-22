import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesAuditComponent } from './sales-audit.component';

describe('SalesAuditComponent', () => {
  let component: SalesAuditComponent;
  let fixture: ComponentFixture<SalesAuditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalesAuditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesAuditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
