import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackConsumptionsComponent } from './track-consumptions.component';

describe('TrackConsumptionsComponent', () => {
  let component: TrackConsumptionsComponent;
  let fixture: ComponentFixture<TrackConsumptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrackConsumptionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrackConsumptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
