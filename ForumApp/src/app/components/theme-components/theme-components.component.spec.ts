import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThemeComponentsComponent } from './theme-components.component';

describe('ThemeComponentsComponent', () => {
  let component: ThemeComponentsComponent;
  let fixture: ComponentFixture<ThemeComponentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ThemeComponentsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ThemeComponentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
