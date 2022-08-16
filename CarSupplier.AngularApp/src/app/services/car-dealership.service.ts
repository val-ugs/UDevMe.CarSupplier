import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap, throwError } from 'rxjs';
import { ICar } from '../models/car';
import { ICarDealership } from '../models/car-dealership';
import { IInputCarDealership } from '../models/input-car-dealership';

@Injectable({
  providedIn: 'root'
})
export class CarDealershipService {
  constructor(
    private http: HttpClient
  ) { }

  carDealerships: ICarDealership[] = []

  getCarDealerships(): Observable<ICarDealership[]> {
    return this.http.get<ICarDealership[]>('https://localhost:7016/api/cardealership/get')
      .pipe(
        tap(carDealerships => this.carDealerships = carDealerships),
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error.message)
        })
      )
  }

  addCarDealerShip(carDealership: IInputCarDealership): Observable<any> {
    return this.http.post('https://localhost:7016/api/cardealership/addcardealership', carDealership)
  }

  addCar(car: ICar): Observable<any> {
    return this.http.post('https://localhost:7016/api/cardealership/addcar', car)
  }
}
