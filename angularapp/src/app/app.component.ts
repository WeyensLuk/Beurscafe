import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Drink } from './models/drink';
import { Routes } from '@angular/router';
import { TrackConsumptionsComponent } from './track-consumptions/track-consumptions.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {


  title = 'Prijslijst';
}


