import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Chart } from './components/chart/chart';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Chart],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected title = 'ui';
}
