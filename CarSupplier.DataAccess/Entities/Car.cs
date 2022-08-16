using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSupplier.DataAccess.MSSQL.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Color { get; set; }
        public int CarDealershipId { get; set; }
        public CarDealership? CarDealership { get; set; }
    }
}
