import { HttpClientModule } from '@angular/common/http';
import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { PriceComponent } from './price/price.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PriceDifferencePipe } from './pipes/price-difference.pipe';
import { TrackConsumptionsComponent } from './track-consumptions/track-consumptions.component';
import { TrackConsumptionDrinkComponent } from './track-consumption-drink/track-consumption-drink.component';
import { InputNumberModule } from 'primeng/inputnumber';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { PricelistComponent } from './pricelist/pricelist.component';

@NgModule({
  declarations: [
    AppComponent,
    PriceComponent,
    PriceDifferencePipe,
    TrackConsumptionsComponent,
    TrackConsumptionDrinkComponent,
    PricelistComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    InputNumberModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
