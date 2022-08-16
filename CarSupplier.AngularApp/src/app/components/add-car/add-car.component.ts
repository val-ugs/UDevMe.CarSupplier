import { HttpErrorResponse } from '@angular/common/http';
import { Component, ErrorHandler } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError } from 'rxjs';
import { CarDealershipService } from 'src/app/services/car-dealership.service';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrls: ['./add-car.component.scss']
})
export class AddCarComponent {
  constructor(
    private carDealershipService: CarDealershipService
  ) {
  }

  isError = false
  apiErrorMessage = ""
  
  form = new FormGroup({
    brand: new FormControl<string>('', [
      Validators.required
    ]),
    color: new FormControl<string>('', [
      Validators.required
    ])
  })

  get brand() {
    return this.form.controls.brand as FormControl
  }
  get color() {
    return this.form.controls.color as FormControl
  }

  submit() {
    if (this.form.valid) {
      this.isError = false;
      this.carDealershipService.addCar({
        brand: this.form.value.brand as string,
        color: this.form.value.color as string
      }).subscribe({
        next: () => setTimeout(() => window.location.reload(), 1000),
        error: (err: HttpErrorResponse) => this.apiErrorMessage = err.error
      })
    }
    else 
      this.isError = true;
  }
}
