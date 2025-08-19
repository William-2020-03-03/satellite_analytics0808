import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BigData } from './big-data';

describe('BigData', () => {
  let component: BigData;
  let fixture: ComponentFixture<BigData>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BigData]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BigData);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
