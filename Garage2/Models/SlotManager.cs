using Garage2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
	public class SlotManager
	{
		List<GarageSlot> slots;

		static SlotManager instance = null;
		bool DBRead = false;
		public static SlotManager Instace { get { if (instance == null) { instance = new SlotManager(); } return instance; } }

		public SlotManager()
		{
			int counter = 0;
			GarageSlot newslot;
			this.slots = new List<GarageSlot>();
			while (counter++ < Globals.GarageSize) {
				newslot = new GarageSlot();
				newslot.SlotId = counter.ToString();
				newslot.InUse = 0;
				slots.Add(newslot);
			}
		}

		public void ReadDB(Garage2.Data.Garage2Context db) {
			if (this.DBRead) return;
			this.DBRead = true;
			string[] slotsused;
			GarageSlot currentslot;
			var result = db.ParkedVehicle.Where(v => v.State == Globals.CheckInState).ToList();
			foreach (var v in result)
			{
				if (v.SlotsInUse != null) {
					slotsused = v.SlotsInUse.Split(',');
					foreach (var slot in slotsused) {
						currentslot = slots.Find(s => s.SlotId == slot);
						if (currentslot != null) {
							currentslot.InUse = 3;
						}
					}
				}
			}
		}

	}
}
