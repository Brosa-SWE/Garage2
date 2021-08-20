using Garage2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.ViewModels
{
	public class SearchViewModel
	{
		// added Id
		public int Id { get; set; }

		[Display(Name = "Vehicle type")]
		public VehicleType VehicleType { get; set; }

		[Display(Name = "License Plate")]
		public string LicensePlate { get; set; }

		public String Make { get; set; }

		public string Model { get; set; }

		[Display(Name = "Arrived Time")]
		public string ArrivalTime { get; set; }

		[Display(Name = "Parked Hours")]
		public string ParkedTime { get; set; }

	}
}
