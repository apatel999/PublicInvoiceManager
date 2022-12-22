import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StoresAllComponent } from './stores-all.component';

describe('StoresAllComponent', () => {
  let component: StoresAllComponent;
  let fixture: ComponentFixture<StoresAllComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StoresAllComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StoresAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
