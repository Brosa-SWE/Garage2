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
            _context = context;
        }

        // GET: ParkedVehicles
        public IActionResult Index(int? id)
        {
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
			{
				result = await _context.ParkedVehicle.Where(v => v.LicensePlate.ToLower().Contains(vehicle.LicensePlate.ToLower())).ToListAsync();
			}
			else result = await _context.ParkedVehicle.ToListAsync();
			foreach (var v in result) {
			if (v.State == Globals.CheckInState)
				found.Add(new SearchViewModel() { Id = v.Id, VehicleType = v.VehicleType, LicensePlate = v.LicensePlate, Make = v.Make, Model = v.Model, ArrivalTime = v.ArrivalTime.ToString(), ParkedTime = $"{((DateTime.Now - v.ArrivalTime).TotalHours)}" });;
			}
			return View(found);
		}

		// CHECKOUT
//		[HttpPost]
		public async Task<IActionResult> CheckOut(int? id)
		{
			var reciept = new CheckOutViewModel();
			if (id != null)
			{
				var parkedVehicle = await _context.ParkedVehicle.FirstOrDefaultAsync(m => m.Id == id);
				if (parkedVehicle != null) {
					if (parkedVehicle.State == Globals.CheckInState) {
						parkedVehicle.State = Globals.CheckOutState;
						parkedVehicle.DepartureTime = DateTime.Now;
						_context.Update(parkedVehicle);
						await _context.SaveChangesAsync();
						int parkMinutes = (int)(parkedVehicle.DepartureTime - parkedVehicle.ArrivalTime).TotalMinutes;
						int amount = (int)(parkMinutes * Globals.ParkingPrice);
						reciept.LicensePlate = parkedVehicle.LicensePlate;
						reciept.CheckinTime = parkedVehicle.ArrivalTime.ToString();
						reciept.CheckoutTime = parkedVehicle.DepartureTime.ToString();
						int minutes = (int)(parkedVehicle.DepartureTime - parkedVehicle.ArrivalTime).TotalMinutes;
						int hours = minutes / 60; minutes %= 60;
						int days = hours / 24; hours %= 24;
						if (days > 0) reciept.ParkTime = $"{days} Days {hours} Hours {minutes} Minutes";
						else if (hours > 0) reciept.ParkTime = $"{hours} Hours {minutes} Minutes";
						else reciept.ParkTime = $"{minutes} Minutes";
						reciept.Amount = $"{parkMinutes} Minutes a {Globals.ParkingPrice} SEK/minute = {amount} SEK (incl moms)";
						return View(reciept);
					}
				}
				return View("CheckOutError");
			}
			return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Details), new { id = id });
            }
            return View(nameof(Overview));
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
    }
}
