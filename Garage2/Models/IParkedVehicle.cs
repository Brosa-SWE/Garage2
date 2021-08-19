using System;

namespace Garage2.Models
{
	public interface IParkedVehicle
	{
		DateTime ArrivalTime { get; set; }
		VehicleColor Color { get; set; }
		DateTime DepartureTime { get; set; }
		int Id { get; set; }
		string LicensePlate { get; set; }
		string Make { get; set; }
		string Model { get; set; }
		string State { get; set; }
		VehicleType VehicleType { get; set; }
		int Wheels { get; set; }
	}
}