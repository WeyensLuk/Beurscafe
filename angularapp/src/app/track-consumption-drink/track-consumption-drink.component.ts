import { Component, Input } from '@angular/core';
import { Drink } from '../models/drink';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-track-consumption-drink',
  templateUrl: './track-consumption-drink.component.html',
  styleUrls: ['./track-consumption-drink.component.css'],
})
export class TrackConsumptionDrinkComponent {
  @Input() drink: Drink;
  private http: HttpClient;

  constructor(_http: HttpClient) {
    this.http = _http;
  }

  putNewAmountPurchased() {
    this.http.put(`${environment.baseURL}/drinks`, this.drink).subscribe(
      (result) => {},
      (error) => console.error(error)
    );
  }
}
