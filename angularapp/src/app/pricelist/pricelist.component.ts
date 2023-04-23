import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Drink } from '../models/drink';
import { Subscription, interval } from 'rxjs';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-pricelist',
  templateUrl: './pricelist.component.html',
  styleUrls: ['./pricelist.component.css'],
})
export class PricelistComponent {
  private subscription: Subscription;
  public drinks?: Drink[];

  constructor(http: HttpClient) {
    const source = interval(10000);
    this.subscription = source.subscribe(_ => this.getAllDrinks(http));

    this.getAllDrinks(http);
  }

  private getAllDrinks(http: HttpClient) {
    http.get<Drink[]>(`${environment.baseURL}/drinks`).subscribe(
      (result) => {
        this.drinks = result;
      },
      (error) => console.error(error)
    );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
