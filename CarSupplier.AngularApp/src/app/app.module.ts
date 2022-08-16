import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http'
import { CarSupplierComponent } from './pages/car-supplier/car-supplier.component';
import { CarDealershipComponent } from './components/car-dealership/car-dealership.component';
import { CarComponent } from './components/car/car.component';
import { AddCarComponent } from './components/add-car/add-car.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddCarDealershipComponent } from './components/add-car-dealership/add-car-dealership.component';

@NgModule({
  declarations: [
    AppComponent,
    CarSupplierComponent,
    CarDealershipComponent,
    CarComponent,
    AddCarComponent,
    AddCarDealershipComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
