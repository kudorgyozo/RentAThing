import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentItemComponent } from './rent-item.component';

describe('RentItemComponent', () => {
  let component: RentItemComponent;
  let fixture: ComponentFixture<RentItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentItemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
