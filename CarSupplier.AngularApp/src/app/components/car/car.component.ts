import { Component, Input } from '@angular/core';
import { ICar } from 'src/app/models/car';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.scss']
})
export class CarComponent {
  @Input() index: number;
  @Input() car: ICar

}
