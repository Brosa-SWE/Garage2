using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using Garage2.Models;
using Garage2.ViewModels;
using Garage2.Models.ViewModels;

namespace Garage2.Controllers
{
	public class ParkedVehiclesController : Controller
	{
		private readonly Garage2Context _context;

		public ParkedVehiclesController(Garage2Context context)
		{
			// test - tom
			_context = context;
			if(Globals.EnableRandomCheckIn) SlotManager.Instace.ReadDB(_context);
		}

		// GET: ParkedVehicles
		public IActionResult Index(int? id)
		{
			if (Globals.EnableRandomCheckIn) return View("IndexDev", SlotManager.Instace.AllSlots());
			return View();
		}

		public async Task<IActionResult> Overview()
		{
			DateTime currentTime = DateTime.Now;

			var model = _context.ParkedVehicle.Select(p => new OverviewViewModel
			{
				Id = p.Id,
				VehicleType = p.VehicleType,
				LicensePlate = p.LicensePlate,
				ArrivalTime = p.ArrivalTime,
				ParkedTime = currentTime - p.ArrivalTime,
				State = p.State
			});

			model = model.Where(m => m.State == Globals.CheckInState);

			   
			return View( await model.ToListAsync());
		}

        public IActionResult AdvancedSearch() => View();

        [HttpPost]
		public async Task<IActionResult> AdvancedSearchList(string Licence, int? VehicleT)
		{
			

			var model = _context.ParkedVehicle.Select(p => new AdvancedSearchViewModel
			{
				Id = p.Id,
				VehicleType = p.VehicleType,
				LicensePlate = p.LicensePlate,
				ArrivalTime = p.ArrivalTime,
				DepartureTime=p.DepartureTime,
				ParkedTime = p.DepartureTime - p.ArrivalTime,
				State = p.State
			});

			
			model = string.IsNullOrWhiteSpace(Licence) ? 
					model : 
					model.Where(m => m.LicensePlate.StartsWith(Licence));

			model = VehicleT==null ? 
					model:
					model.Where(m => (int)m.VehicleType == VehicleT);

			model = model.OrderBy(m => m.LicensePlate);
			return View(await model.ToListAsync());
		}

		// GET: ParkedVehicles/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			
			var parkedVehicle = await _context.ParkedVehicle
				.FirstOrDefaultAsync(m => m.Id == id);
			if (parkedVehicle == null)
			{
				return NotFound();
			}

			return View(parkedVehicle);
		}

		// GET: ParkedVehicles/Create
		public IActionResult CheckIn()
		{
			return View();
		}

		// SEARCH
		public async Task<IActionResult> Search([Bind("LicensePlate")] ParkedVehicle vehicle)
		{
			var found = new List<SearchViewModel>();
			IEnumerable<ParkedVehicle> result;
			if ((vehicle != null) && (vehicle.LicensePlate != null) && (vehicle.LicensePlate.Length > 0))
				result = await _context.ParkedVehicle.Where(v => ((v.State == Globals.CheckInState) && (v.LicensePlate.ToLower().Contains(vehicle.LicensePlate.ToLower())))).ToListAsync();
			else result = await _context.ParkedVehicle.Where(v => v.State == Globals.CheckInState).ToListAsync();
			foreach (var v in result) {
				found.Add(new SearchViewModel() { Id = v.Id, VehicleType = v.VehicleType, LicensePlate = v.LicensePlate, Make = v.Make, Model = v.Model, ArrivalTime = v.ArrivalTime.ToString(), ParkedTime = DateTime.Now.TimedDiffString(v.ArrivalTime) });;
			}
			return View(found);
		}

		// CHECKOUT
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CheckOut([Bind("LicensePlate")] ParkedVehicle vehicle)
		{
			if (vehicle == null) return RedirectToAction(nameof(Index));
			if (vehicle.LicensePlate == null) return RedirectToAction(nameof(Index));
			var parkedVehicle = await _context.ParkedVehicle.FirstOrDefaultAsync(m => ((m.LicensePlate == vehicle.LicensePlate) && (m.State == Globals.CheckInState)));
			if (parkedVehicle == null) return View("CheckOutError");

			var reciept = new CheckOutViewModel();
			parkedVehicle.State = Globals.CheckOutState;
			parkedVehicle.DepartureTime = DateTime.Now;
			_context.Update(parkedVehicle);
			await _context.SaveChangesAsync();
			int parkMinutes = (int)(parkedVehicle.DepartureTime - parkedVehicle.ArrivalTime).TotalMinutes;
			int amount = (int)(parkMinutes * Globals.ParkingPrice);
			reciept.LicensePlate = parkedVehicle.LicensePlate;
			reciept.CheckinTime = parkedVehicle.ArrivalTime.ToString();
			reciept.CheckoutTime = parkedVehicle.DepartureTime.ToString();
			reciept.ParkTime = parkedVehicle.DepartureTime.TimedDiffString(parkedVehicle.ArrivalTime);						
			reciept.Amount = $"Total price {amount} SEK (incl moms), at Rate {Globals.ParkingPrice} SEK/minute";
			return View(reciept);
		}




		// POST: ParkedVehicles/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CheckIn([Bind("Id,VehicleType,LicensePlate,Color,Make,Model,Wheels")] ParkedVehicle parkedVehicle)
		{
			parkedVehicle.ArrivalTime = DateTime.Now;
			parkedVehicle.State = Globals.CheckInState;

			if (ModelState.IsValid)
			{
				_context.Add(parkedVehicle);
				await _context.SaveChangesAsync();

				var confirmation = new CheckInConfirmationViewModel();

				confirmation.LicensePlate = parkedVehicle.LicensePlate;
				confirmation.CheckinTime = parkedVehicle.ArrivalTime.ToString();
				return View("CheckInConfirmation", confirmation);
				// return RedirectToAction(confirmation);

				// return RedirectToAction(nameof(Index));
			}

			 return View(parkedVehicle);
		}

		// GET: ParkedVehicles/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
			if (parkedVehicle == null)
			{
				return NotFound();
			}
			return View(parkedVehicle);
		}

	
		// POST: ParkedVehicles/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,LicensePlate,Color,Make,Model,Wheels,ArrivalTime,State")] ParkedVehicle parkedVehicle)
		{
			
			if (id != parkedVehicle.Id)
			{
				return NotFound();
			}
			if (parkedVehicle.State == Globals.CheckOutState)
			{

				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(parkedVehicle);
					await _context.SaveChangesAsync();
					
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ParkedVehicleExists(parkedVehicle.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				TempData["msg"] = "Succesfully Edited";
				return RedirectToAction(nameof(Details),new { id=id});
			}
			return View("Overview");
		}

		// GET: ParkedVehicles/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var parkedVehicle = await _context.ParkedVehicle
				.FirstOrDefaultAsync(m => m.Id == id);
			if (parkedVehicle == null)
			{
				return NotFound();
			}

			return View(parkedVehicle);
		}

		// POST: ParkedVehicles/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
			_context.ParkedVehicle.Remove(parkedVehicle);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ParkedVehicleExists(int id)
		{
			return _context.ParkedVehicle.Any(e => e.Id == id);
		}

        public async Task<IActionResult> CheckLicensePlate(string LicensePlate)
        {
			// 

			var vehicleModel = await _context.ParkedVehicle.FirstOrDefaultAsync(e => e.LicensePlate == LicensePlate);

				if (vehicleModel == null)
            {
                return Json(true);
            }
            else
            {
                if (vehicleModel.State == Globals.CheckInState)
                {
                return Json("Vehicle with license plate " + LicensePlate + " is already parked.");
                }
                else
                {
                    return Json(true);
                }
            }

		}

      

		public async Task<IActionResult> CheckInRandomVehicles(int number)
		{
			if (!Globals.EnableRandomCheckIn) return RedirectToAction(nameof(Index));
			Random rnd = new Random();
			ParkedVehicle newVehicle;
			int newvehicles = number, counter = 0;
			if (newvehicles <= 0) return RedirectToAction(nameof(Index));
			if (newvehicles > 100) newvehicles = 100;

			while (counter < newvehicles)
			{
				newVehicle = new ParkedVehicle();
				newVehicle.LicensePlate = "";
				newVehicle.LicensePlate += (char)rnd.Next(65, 90);
				newVehicle.LicensePlate += (char)rnd.Next(65, 90);
				newVehicle.LicensePlate += (char)rnd.Next(65, 90);
				newVehicle.LicensePlate += rnd.Next(100, 999).ToString();
				if (this.ParkedVehicleExist(newVehicle.LicensePlate) == null)
				{
					newVehicle.State = Globals.CheckInState;
					newVehicle.VehicleType = (VehicleType)rnd.Next(minValue: 1, maxValue: (int)VehicleType.Truck);
					newVehicle.Wheels = rnd.Next(minValue: 1, maxValue: 8);
					newVehicle.Color = (VehicleColor)rnd.Next(minValue: 0, maxValue: (int)VehicleColor.Silver);
					switch (rnd.Next(0, 5))
					{
						case 0: newVehicle.Make = "Saab"; newVehicle.Model = "900"; break;
						case 1: newVehicle.Make = "Volvo"; newVehicle.Model = "V60s"; break;
						case 2: newVehicle.Make = "Lamborghini"; newVehicle.Model = "SX"; break;
						case 3: newVehicle.Make = "BMW"; newVehicle.Model = "xi"; break;
						case 4: newVehicle.Make = "Toyota"; newVehicle.Model = "Panda"; break;
						case 5: newVehicle.Make = "Chrysler"; newVehicle.Model = "gt"; break;
					}
					newVehicle.ArrivalTime = DateTime.Now;
					newVehicle.ArrivalTime = newVehicle.ArrivalTime.AddDays(-rnd.Next(0, 1));
					newVehicle.ArrivalTime = newVehicle.ArrivalTime.AddHours(-rnd.Next(1, 3));
					newVehicle.ArrivalTime = newVehicle.ArrivalTime.AddMinutes(-rnd.Next(1, 3600));
					newVehicle.SlotsInUse = SlotManager.Instace.GetSlots(newVehicle.VehicleType);
					if (newVehicle.SlotsInUse != null)
					{
						_context.Add(newVehicle);
						counter++;
					}
					else break;	// as was not able to get a parking slot - expect garage to be near full
				}
			}
			await _context.SaveChangesAsync();
			if (counter > 0) return RedirectToAction(nameof(Search));
			return RedirectToAction(nameof(Index));
		}

		private ParkedVehicle ParkedVehicleExist(string licensplate)
		{
			if (licensplate == null) return null;
			var vehicle = _context.ParkedVehicle.FirstOrDefault(p => p.LicensePlate.ToUpper() == licensplate.ToUpper());
			return vehicle;
		}

		private ParkedVehicle ParkedVehicleParked(string licensplate)
		{
			if (licensplate == null) return null;
			var vehicle = _context.ParkedVehicle.FirstOrDefault(p => ((p.LicensePlate.ToUpper() == licensplate.ToUpper()) && (p.State == Globals.CheckInState)));
			return vehicle;
		}
	}
}
