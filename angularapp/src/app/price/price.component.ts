import { Component, Input } from '@angular/core';
import { Drink } from '../models/drink';

@Component({
  selector: 'app-price',
  templateUrl: './price.component.html',
  styleUrls: ['./price.component.css']
})
export class PriceComponent {
  @Input() drink: Drink | any;
  percentage: number | any;

  ngOnInit() {
    this.percentage = this.drink.currentPrice / this.drink.originalPrice / 2;
  }

  calculateBackgroundColor() {
    const hue = ((1-this.percentage) * 120).toString(10);
    return `hsl(${hue}, 100%, 75%)`;
  }

  calculateWidth() {
    return `${this.percentage*100}vw`
  }

  calculatePriceDifference() {
    const difference = this.drink.originalPrice - this.drink.currentPrice;
    return difference;
  }
}
