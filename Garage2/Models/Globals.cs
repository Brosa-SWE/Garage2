﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
	static public class Globals
	{
		public static readonly bool EnableRandomCheckIn = true;			// Indicate if in production or not
		public static readonly double ParkingPrice = 0.99;				// SEK per minute
		public static readonly string CheckInState = "parked";			// State description for parked vehicle
		public static readonly string CheckOutState = "unparked";		// State description for checked out vehicle
	}
}
