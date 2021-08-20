using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models.ViewModels
{
    public class DetailViewModel
    {
		public int Id { get; set; }
		public VehicleType VehicleType { get; set; }
		public string LicensePlate { get; set; }
		public VehicleColor Color { get; set; }
		public String Make { get; set; }
		public string Model { get; set; }
		public int Wheels { get; set; }
		public DateTime ArrivalTime { get; set; }
		[Display(Name = "Parked Time")]
		public TimeSpan ParkedTime { get; set; }
	
	}
}
