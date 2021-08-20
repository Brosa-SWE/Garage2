using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
	static public class Extentions
	{
		public static string TimedDiffString(this DateTime DT, DateTime DT2) {
			int minutes = (int)(DT - DT2).TotalMinutes;
			int hours = minutes / 60; minutes %= 60;
			int days = hours / 24; hours %= 24;
			if (days > 0) return $"{days} Days {hours} Hours {minutes} Minutes";
			else if (hours > 0) return $"{hours} Hours {minutes} Minutes";
			return $"{minutes} Minutes";
		}
	}
}
