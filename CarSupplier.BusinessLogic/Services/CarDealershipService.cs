using CarSupplier.BusinessLogic.Exceptions;
using CarSupplier.Domain.Abstractions;
using CarSupplier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.BusinessLogic.Services
{
    /// <summary>
    /// Car Dealership Service performs actions with car dealerships
    /// </summary>
    public class CarDealershipService : ICarDealershipService
    {
        // probality Equal Max Number Of Same Cars
        private const float P0 = 0.5f;
        // probality Less One Max Number Of Same Cars
        private const float P1 = 0.666f;
        // probality Less Two Max Number Of Same Cars
        private const float P2 = 0.8f;
        // probality Less Three Max Number Of Same Cars
        private const float P3 = 1f;
        // maximum difference between the number of cars
        private const int MaxDifference = 3;

        private readonly ICarDealershipRepository _carDealershipRepository;

        public CarDealershipService(ICarDealershipRepository carDealershipRepository)
        {
            _carDealershipRepository = carDealershipRepository;
        }

        /// <summary>
        /// Add car dealership
        /// </summary>
        /// <param name="carDealership"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddCarDealership(CarDealership carDealership)
        {
            if (carDealership == null)
                throw new ArgumentNullException("Автосалона не существует");

            if (string.IsNullOrEmpty(carDealership.Name))
                throw new ArgumentNullException("Название автосалона не указана");

            if (carDealership.MaxNumberOfCars <= 0)
                throw new Exception("Автосалон должен содержать как минимум 1 машину");

            _carDealershipRepository.AddCarDealership(carDealership);
        }

        /// <summary>
        /// Add a car to an available dealership
        /// </summary>
        /// <param name="car"></param>
        public void AddCar(Car car)
        {
            if (car == null)
                throw new ArgumentNullException("Машина не существует");

            if (string.IsNullOrEmpty(car.Brand))
                throw new ArgumentNullException("Марка не указана");

            if (string.IsNullOrEmpty(car.Color))
                throw new ArgumentNullException("Цвет не указан");

            var availableCarDealerships = _carDealershipRepository.GetAvailableCarDealerships();

            if (availableCarDealerships == null)
                throw new NotFoundException("Автосалонов не существует");

            var numberOfAvailableCarDealerships = availableCarDealerships.Count();

            if (numberOfAvailableCarDealerships == 0)
                throw new Exception("Нет доступных мест у автосалонов");

            int selectedCarDealershipId = 0; // initialization

            if (numberOfAvailableCarDealerships == 1)
                selectedCarDealershipId = availableCarDealerships[0].Id;
            else
            {
                
                Dictionary<int, int> CarDealershipIdsWithNumberOfSameCars = availableCarDealerships
                    .ToDictionary(x => x.Id,
                                  // the number of same cars matching the features of the car
                                  x => x.Cars
                                    .Where(y => y.Equals(car))
                                    .Count());

                int maxNumberOfSameCars = CarDealershipIdsWithNumberOfSameCars.Select(x => x.Value).Max();

                // number of car dealerships relative to the difference between the current number of same cars
                // and the maximum number of same cars
                // array index - difference between the number of same cars and the max number of same cars
                // if the difference is greater than MaxDifference, then we take it as MaxDifference
                // array value - number of car dealerships
                int[] numberOfCarDealershipsRelativeDifference = new int[MaxDifference + 1];
                
                for (int i = 0; i < CarDealershipIdsWithNumberOfSameCars.Count(); i++)
                {
                    int differenceMaxAndCurrent = maxNumberOfSameCars - CarDealershipIdsWithNumberOfSameCars.Values.ElementAt(i);
                    
                    if (differenceMaxAndCurrent >= MaxDifference)
                        numberOfCarDealershipsRelativeDifference[MaxDifference]++;
                    else
                        numberOfCarDealershipsRelativeDifference[differenceMaxAndCurrent]++;
                }

                Dictionary<int, float> carDealershipIdsWithProbabilities;

                if (numberOfCarDealershipsRelativeDifference[MaxDifference] > 0)
                {
                    // if the difference is MaxDifference
                    carDealershipIdsWithProbabilities = CarDealershipIdsWithNumberOfSameCars
                        .Where(x => maxNumberOfSameCars - x.Value >= MaxDifference)
                        .ToDictionary(x => x.Key, x => P3 / numberOfCarDealershipsRelativeDifference[MaxDifference]);
                }
                else
                {
                    // if the difference is less than 3
                    int minNumberOfSameCars = CarDealershipIdsWithNumberOfSameCars.Select(x => x.Value).Min();

                    carDealershipIdsWithProbabilities = CarDealershipIdsWithNumberOfSameCars
                        .ToDictionary(x => x.Key,
                                      x => CalculateProbability(maxNumberOfSameCars - minNumberOfSameCars,
                                                                maxNumberOfSameCars - x.Value,
                                                                numberOfCarDealershipsRelativeDifference));
                }

                if (carDealershipIdsWithProbabilities.Count() == 1)
                    selectedCarDealershipId = carDealershipIdsWithProbabilities.ElementAt(0).Key;
                else
                {
                    Random random = new Random();
                    var probality = random.NextDouble(); // return a random number between 0 and 1

                    for (int i = 0; i < carDealershipIdsWithProbabilities.Count; i++)
                    {
                        var carDealershipProbality = carDealershipIdsWithProbabilities.ElementAt(i).Value;
                        if (probality <= carDealershipProbality)
                        {
                            selectedCarDealershipId = carDealershipIdsWithProbabilities.ElementAt(i).Key;
                            break;
                        }

                        probality -= carDealershipProbality;
                    }
                }
            }

            _carDealershipRepository.AddCarToCarDealership(selectedCarDealershipId, car);
        }

        /// <summary>
        /// Calculate the probability of a car dealership
        /// </summary>
        /// <param name="differenceMaxAndMin"> difference between the maximum and minimum number of same cars </param>
        /// <param name="differenceMaxAndCurrent"> difference between the maximum and current number of same cars </param>
        /// <param name="numberOfCarDealershipsRelativeDifference"></param>
        /// <returns> probability </returns>
        private float CalculateProbability(int differenceMaxAndMin, int differenceMaxAndCurrent, int[] numberOfCarDealershipsRelativeDifference)
        {
            float p = 1f; // initial probability

            if (differenceMaxAndMin == differenceMaxAndCurrent && differenceMaxAndMin != 0)
                p *= CalculateProbability(differenceMaxAndMin, differenceMaxAndCurrent);
            else
                while (differenceMaxAndMin != differenceMaxAndCurrent)
                {
                    p *= CalculateProbability(differenceMaxAndMin, differenceMaxAndCurrent);

                    differenceMaxAndMin--;

                    // isf there are no car dealerships with a less difference
                    if (numberOfCarDealershipsRelativeDifference[differenceMaxAndMin] == 0)
                        break;
                }

            p /= numberOfCarDealershipsRelativeDifference[differenceMaxAndCurrent];

            return p;
        }

        /// <summary>
        /// Calculate the distribution probability
        /// </summary>
        /// <param name="differenceMaxAndMin"></param>
        /// <param name="differenceMaxAndCurrent"></param>
        /// <returns> probability </returns>
        private float CalculateProbability(int differenceMaxAndMin, int differenceMaxAndCurrent)
        {
            return (differenceMaxAndMin) switch
            {
                0 => (differenceMaxAndMin == differenceMaxAndCurrent) ? P0 : (1 - P0),
                1 => (differenceMaxAndMin == differenceMaxAndCurrent) ? P1 : (1 - P1),
                2 => (differenceMaxAndMin == differenceMaxAndCurrent) ? P2 : (1 - P2),
                _ => 1
            };
        }

        /// <summary>
        /// Get car dealerships
        /// </summary>
        /// <returns> car dealerships </returns>
        public List<CarDealership> GetCarDealerships()
        {
            return _carDealershipRepository.GetCarDealerships();
        }
    }
}