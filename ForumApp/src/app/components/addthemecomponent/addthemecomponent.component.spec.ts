import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddthemecomponentComponent } from './addthemecomponent.component';

describe('AddthemecomponentComponent', () => {
  let component: AddthemecomponentComponent;
  let fixture: ComponentFixture<AddthemecomponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddthemecomponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddthemecomponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
