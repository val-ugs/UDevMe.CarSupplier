import { Component, OnInit } from '@angular/core';
import { CarDealershipService } from 'src/app/services/car-dealership.service';

@Component({
  selector: 'app-car-supplier',
  templateUrl: './car-supplier.component.html',
  styleUrls: ['./car-supplier.component.scss']
})
export class CarSupplierComponent implements OnInit {
  loading = false

  constructor(
    public carDealershipService: CarDealershipService
  ) { }

  ngOnInit(): void {
    this.loading = true

    this.carDealershipService.getCarDealerships().subscribe(
      () => this.loading = false
    )
  }

}
