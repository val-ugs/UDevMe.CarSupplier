import { ICar } from "./car";

export interface ICarDealership {
    id: number;
    name: string;
    maxNumberOfCars: number;
    cars: Array<ICar>;
}