import { CurrencyPipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'priceDifference'
})
export class PriceDifferencePipe extends CurrencyPipe implements PipeTransform {
  override transform(value: number | string | null | undefined): null;
  override transform(value: number, ...args: unknown[]): string {
    if(value >= 0) return `-${super.transform(value, 'EUR')}`;
    return `+${super.transform(value * -1, 'EUR')}`;
  }

}
