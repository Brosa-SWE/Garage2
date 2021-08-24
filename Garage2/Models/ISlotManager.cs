using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
	public interface ISlotManager
	{
		public string GetSlotId(VehicleType vehicletype);
		public void ReturnSlotId(string slotid);
	}
}
