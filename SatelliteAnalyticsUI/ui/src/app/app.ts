import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Chart } from './components/chart/chart';
import { BigData } from './components/big-data/big-data';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Chart, BigData],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected title = 'ui';
}
