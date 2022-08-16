using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.DataAccess.MSSQL.Entities
{
    public class CarDealership
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MaxNumberOfCars { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
