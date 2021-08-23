using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Garage2
{
	public enum VehicleType
	{
		[Display(Name = " ")]_,
		Car,
		Motorcycle,
		Bus,
		Boat,
		Truck

	}
}
