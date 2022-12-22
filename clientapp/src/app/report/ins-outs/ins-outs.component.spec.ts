import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InsOutsComponent } from './ins-outs.component';

describe('InsOutsComponent', () => {
  let component: InsOutsComponent;
  let fixture: ComponentFixture<InsOutsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InsOutsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InsOutsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
