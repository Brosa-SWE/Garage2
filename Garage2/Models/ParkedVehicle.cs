using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
	public class ParkedVehicle
	{
		// added Id
		public int Id { get; set; }
		

		// Here Vehicletype is of type VehicleType - Constraint might need to be changed from stringlength()
		[Required]
		[StringLength(30)]
		// Car, Motorcycle etc. 
		public VehicleType VehicleType { get; set; }
		
		//	Here we might need to add constraint min 3 max 15 ?
		[Required]
		public string LicensePlate { get; set; }

		// Here Maybe change this property to an enum isstead, like Vehicle type ?
		public string Color { get; set; }

		// Here maybe limit length ?
		[Required]
		// Volvo, Audi etc.
		public String Make { get; set; }

		// Here we might add min/max length
		// V90, A6 etc.
		public string Model { get; set; }

		[Required]
		[Range(1, 6)]
		public int Wheels { get; set; }

		[DataType(DataType.Time)]
		[Display(Name = "Arrived Time")]
		public DateTime ArrivalTime { get; set; }

		// here: Add an Departure time ? 

		// here: Maybe change this property also to an enum, with defined states ?
		// Parked or "Unparked / Removed from Garage" 
		public String State { get; set; }

 
	}
}
