using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.Domain.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Color { get; set; }

        public override bool Equals(object? obj)
        {
            var anotherCar = obj as Car;

            if (anotherCar == null)
                return false;

            return this.Brand?.ToLower() == anotherCar.Brand?.ToLower()
                && this.Color?.ToLower() == anotherCar.Color?.ToLower();
        }
    }
}
