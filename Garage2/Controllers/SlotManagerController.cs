using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using Garage2.Models;

namespace Garage2.Controllers
{
    public class SlotManagerController : Controller, ISlotManager
    {
        private readonly Garage2Context _context;

        public SlotManagerController(Garage2Context context)
        {
            _context = context;
			this.BuildSlots();
        }

		private void BuildSlots() {
			// This is called from the constructor and make sure DB table has the required amount of 
			// Parking slots available as per Globals.GarageSize, if not, it will create them
			// SlotID format :
		}


        // GET: GarageSlots
        public async Task<IActionResult> Index()
        {
            return View(await _context.GarageSlot.ToListAsync());
        }

        // GET: GarageSlots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garageSlot = await _context.GarageSlot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garageSlot == null)
            {
                return NotFound();
            }

            return View(garageSlot);
        }

        // GET: GarageSlots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GarageSlots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SlotId,InUse,BlockId")] GarageSlot garageSlot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garageSlot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garageSlot);
        }

        // GET: GarageSlots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garageSlot = await _context.GarageSlot.FindAsync(id);
            if (garageSlot == null)
            {
                return NotFound();
            }
            return View(garageSlot);
        }

        // POST: GarageSlots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SlotId,InUse,BlockId")] GarageSlot garageSlot)
        {
            if (id != garageSlot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garageSlot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarageSlotExists(garageSlot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(garageSlot);
        }

        // GET: GarageSlots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garageSlot = await _context.GarageSlot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garageSlot == null)
            {
                return NotFound();
            }

            return View(garageSlot);
        }

        // POST: GarageSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var garageSlot = await _context.GarageSlot.FindAsync(id);
            _context.GarageSlot.Remove(garageSlot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarageSlotExists(int id)
        {
            return _context.GarageSlot.Any(e => e.Id == id);
        }

		public string GetSlotId(VehicleType vehicletype)
		{
			throw new NotImplementedException();
		}

		public void ReturnSlotId(string slotid)
		{
			throw new NotImplementedException();
		}
	}
}
