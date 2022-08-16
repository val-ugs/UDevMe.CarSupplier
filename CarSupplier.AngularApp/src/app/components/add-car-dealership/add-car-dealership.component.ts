import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CarDealershipService } from 'src/app/services/car-dealership.service';

@Component({
  selector: 'app-add-car-dealership',
  templateUrl: './add-car-dealership.component.html',
  styleUrls: ['./add-car-dealership.component.scss']
})
export class AddCarDealershipComponent{
  constructor(
    private carDealershipService: CarDealershipService
  ) {
  }

  isError = false
  apiErrorMessage = ""
  
  form = new FormGroup({
    name: new FormControl<string>('', [
      Validators.required
    ]),
    maxNumberOfCars: new FormControl<number>(0, [
      Validators.required
    ])
  })

  get name() {
    return this.form.controls.name as FormControl
  }
  get maxNumberOfCars() {
    return this.form.controls.maxNumberOfCars as FormControl
  }

  submit() {
    if (this.form.valid) {
      this.isError = false;
      this.carDealershipService.addCarDealerShip({
        name: this.form.value.name as string,
        maxNumberOfCars: this.form.value.maxNumberOfCars as number
      }).subscribe({
        next: () => setTimeout(() => window.location.reload(), 1000),
        error: (err: HttpErrorResponse) => this.apiErrorMessage = err.error
      })
    }
    else 
      this.isError = true;
  }
}
