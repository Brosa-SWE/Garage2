using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
	public class GarageSlot
	{
		public string SlotId { get; set; }	// reflect 1/3 of a parking spot
		public int InUse { get; set; }     // reflect how much of the slot is used up (0=free, 3= fully occuiped )
	}
}
