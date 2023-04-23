import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackConsumptionDrinkComponent } from './track-consumption-drink.component';

describe('TrackConsumptionDrinkComponent', () => {
  let component: TrackConsumptionDrinkComponent;
  let fixture: ComponentFixture<TrackConsumptionDrinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrackConsumptionDrinkComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrackConsumptionDrinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
