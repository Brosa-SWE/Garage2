using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
    public class ParkedVehicle
    {
        // Car, Motorcycle etc. 
        public string VehicleType { get; set; }

        public string LicensePlate { get; set; }

        public string Color { get; set; }

        // Volvo, Audi etc.
        public String Make { get; set; }

        // V90, A6 etc.
        public string Model { get; set; }

        public int Wheels { get; set; }

        public DateTime ArrivalTime { get; set; }

        // Parked or "Unparked / Removed from Garage" 
        public String State { get; set; }

 
    }
}
