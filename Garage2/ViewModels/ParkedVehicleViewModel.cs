using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Garage2.ViewModels
{
    public class ParkedVehicleViewModel
    {

		// added Id
		public int Id { get; set; }


		// Removed constraints since this is now an ENUM
		// Car, Motorcycle etc. 
		[Required]
		[Range(1,20, ErrorMessage = "You must select a vehicle type")]
		public VehicleType VehicleType { get; set; }

		// Added limit	
		[StringLength(15)]
		[Required]
		[Remote(action: "CheckLicensePlate", controller: "ParkedVehicles")]
		public string LicensePlate { get; set; }

		// Changed to an enum instead
		public VehicleColor Color { get; set; }

		// Added max length
		[StringLength(30)]
		[Required]
		// Volvo, Audi etc.
		public String Make { get; set; }

		// Added max length
		// V90, A6 etc.
		[StringLength(30)]
		public string Model { get; set; }

		[Required]
		[Range(1, 6)]
		public int Wheels { get; set; }

		[DataType(DataType.Time)]
		public DateTime ArrivalTime { get; set; }

		[DataType(DataType.Time)]
		public DateTime DepartureTime { get; set; }

		// Parked or "Unparked / Removed from Garage" 
		public String State { get; set; }

	}
}
