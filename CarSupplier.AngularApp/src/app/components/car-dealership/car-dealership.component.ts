import { Component, Input, OnInit } from '@angular/core';
import { ICarDealership } from 'src/app/models/car-dealership';


@Component({
  selector: 'app-car-dealership',
  templateUrl: './car-dealership.component.html',
  styleUrls: ['./car-dealership.component.scss']
})
export class CarDealershipComponent{
  @Input() carDealership: ICarDealership

  details = true
}
