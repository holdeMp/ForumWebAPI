import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThemeContentComponentComponent } from './theme-content-component.component';

describe('ThemeContentComponentComponent', () => {
  let component: ThemeContentComponentComponent;
  let fixture: ComponentFixture<ThemeContentComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ThemeContentComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ThemeContentComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
