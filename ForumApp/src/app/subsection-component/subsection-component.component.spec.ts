import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubsectionComponentComponent } from './subsection-component.component';

describe('SubsectionComponentComponent', () => {
  let component: SubsectionComponentComponent;
  let fixture: ComponentFixture<SubsectionComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubsectionComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubsectionComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
