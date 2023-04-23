import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Drink } from '../models/drink';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-track-consumptions',
  templateUrl: './track-consumptions.component.html',
  styleUrls: ['./track-consumptions.component.css']
})
export class TrackConsumptionsComponent {
  public drinks?: Drink[];

  constructor(http: HttpClient) {
    http.get<Drink[]>(`${environment.baseURL}/drinks`).subscribe(result => {
      this.drinks = result;
    }, error => console.error(error));
  }
}
