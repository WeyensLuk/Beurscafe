import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrackConsumptionsComponent } from './track-consumptions/track-consumptions.component';
import { PricelistComponent } from './pricelist/pricelist.component';

const routes: Routes = [
  { path: '', component: PricelistComponent },
  { path: 'consumptions', component: TrackConsumptionsComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
