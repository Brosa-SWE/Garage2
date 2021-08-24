using Garage2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2.Models
{
	public class SlotManager
	{
		List<GarageSlot> parkingslots;
		static SlotManager instance = null;
		bool DBRead = false;

		public static SlotManager Instace { get { if (instance == null) { instance = new SlotManager(); } return instance; } }

		private SlotManager()
		{
			int counter = 0;
			GarageSlot newslot;
			this.parkingslots = new List<GarageSlot>();
			while (counter++ < Globals.GarageSize) {
				newslot = new GarageSlot();
				newslot.SlotId = counter.ToString();
				newslot.InUse = 0;
				parkingslots.Add(newslot);
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
					slotsused = v.SlotsInUse.Split(' ');
					foreach (var slot in slotsused) {
						currentslot = parkingslots.Find(s => s.SlotId == slot);
						if (currentslot != null) {
							if (v.VehicleType == VehicleType.Motorcycle) currentslot.InUse++;
							else currentslot.InUse = 3;
						}
					}
				}
			}
		}

		public IEnumerable<GarageSlot> AllSlots() {
			foreach (var ps in this.parkingslots) {
				yield return ps;
			}
		}

		private static int SlotsNeeded(VehicleType vehicletype) {
			int spaceneeded = -1;
			// each parkings space consists of 3 slots (as we need to be able to place motorcycle)
			switch (vehicletype)
			{
				case VehicleType.Car: spaceneeded = 3; break;   // 1 parking space
				case VehicleType.Motorcycle: spaceneeded = 1; break;    // 1/3 of a parking space
				case VehicleType.Bus: spaceneeded = 9; break;   // 3 parking spaces
				case VehicleType.Truck: spaceneeded = 12; break;    // 4 parking spaces
				case VehicleType.Boat: spaceneeded = 6; break;  // 2 parking spaces
			}
			return spaceneeded;
		}

		// return a string with slotid's reserved for vehicle
		// or return null if not able to park
		public string GetSlots(VehicleType vehicletype) {

			int spaceneeded = SlotsNeeded(vehicletype);
			if (spaceneeded < 0) return null;  // Internal bug - an unknown vehicle type

			var accumulatedslots = new List<GarageSlot>();
			int accumulatedslotscount = 0;
			foreach (var ps in this.parkingslots) {
				if (ps.InUse == 3)
				{
					accumulatedslotscount = 0;
					accumulatedslots.Clear();
				}
				else if (spaceneeded == 1) {
					accumulatedslots.Add(ps);
					accumulatedslotscount = 1;
					break;
				}
				else if (ps.InUse == 0)	{
					accumulatedslots.Add(ps);
					accumulatedslotscount += 3;
					if (accumulatedslotscount >= spaceneeded) break;
				}
			}

			string returningslots = "";
			if (accumulatedslotscount >= spaceneeded) {
				foreach (var ps in accumulatedslots) {
					returningslots += ps.SlotId + " ";
					if (spaceneeded == 1) ps.InUse++;
					else ps.InUse = 3;   // fully 
				}
			}

			if (returningslots.Length == 0) return null;
			return returningslots;
		}

		public void ReturnSlots(string parikngslots) { 
			// recieves string with slots "slotid,slotid,slotid"
			// check trought list and mark the solts as free again
		}

		public bool CanPark(VehicleType vehicletype)
		{
			// A query that return true if a vehicle type can be parked
			// Not implemented yet
			return false;
		}

		public int FreeParkingSpaces()
		{
			int counter = 0;
			foreach (var ps in this.parkingslots) {
				if (ps.InUse == 0) counter++;
			}
			return counter;
		}
	}
}
