using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
    public class ParkedVehicle
    {
        
        [Required]
        [StringLength(30)]
        // Car, Motorcycle etc. 
        public string VehicleType { get; set; }

        [Required]
        public string LicensePlate { get; set; }


        public string Color { get; set; }

        [Required]
        // Volvo, Audi etc.
        public String Make { get; set; }

        // V90, A6 etc.
        public string Model { get; set; }

        [Required]
        [Range(1, 6)]
        public int Wheels { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Arrived Time")]
        public DateTime ArrivalTime { get; set; }

        // Parked or "Unparked / Removed from Garage" 
        public String State { get; set; }

 
    }
}
