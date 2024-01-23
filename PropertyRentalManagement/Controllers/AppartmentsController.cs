using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PropertyRentalManagement.Models;

namespace PropertyRentalManagement.Controllers
{
    public class AppartmentsController : Controller
    {
        private readonly FinalProjectDbContext _context;

        public AppartmentsController(FinalProjectDbContext context)
        {
            _context = context;
        }

        // GET: Appartments
        public async Task<IActionResult> Index()
        {
            var finalProjectDbContext = _context.Appartments.Include(a => a.Building).Include(a => a.Manager).Include(a => a.Owner).Include(a => a.Status);
            return View(await finalProjectDbContext.ToListAsync());
        }

        public async Task<IActionResult> TenantViewAsync()
        {
            var finalProjectDbContext = _context.Appartments.Include(a => a.Building).Include(a => a.Manager).Include(a => a.Owner).Include(a => a.Status);
            return View(await finalProjectDbContext.ToListAsync());

        }

        [HttpPost]
        public async Task<IActionResult> TenantViewAsync(int Rent, int numberOfRooms)
        {
            var finalProjectDbContext = _context.Appartments.Include(a => a.Building).Include(a => a.Manager).Include(a => a.Owner).Include(a => a.Status); ;
            if (Rent > 0)
            {
                finalProjectDbContext = _context.Appartments.Where(s => s.AptRent <= Rent).Include(a => a.Building).Include(a => a.Manager).Include(a => a.Owner).Include(a => a.Status); ;
            }
            if (numberOfRooms > 0)
            {
                finalProjectDbContext = _context.Appartments.Where(s => s.AptNumberOfRooms >= numberOfRooms).Include(a => a.Building).Include(a => a.Manager).Include(a => a.Owner).Include(a => a.Status); ;
            }
            return View(await finalProjectDbContext.ToListAsync());
        }

            // GET: Appartments/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appartment = await _context.Appartments
                .Include(a => a.Building)
                .Include(a => a.Manager)
                .Include(a => a.Owner)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (appartment == null)
            {
                return NotFound();
            }

            return View(appartment);
        }

        // GET: Appartments/Create
        public IActionResult Create()
        {
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId");
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return View();
        }

        // POST: Appartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,StatusId,OwnerId,AptNumber,AptSize,AptRent,AptNumberOfRooms,AptDescription,BuildingId,ManagerId")] Appartment appartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", appartment.BuildingId);
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserId", appartment.ManagerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserId", appartment.OwnerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", appartment.StatusId);
            return View(appartment);
        }

        // GET: Appartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appartment = await _context.Appartments.FindAsync(id);
            if (appartment == null)
            {
                return NotFound();
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", appartment.BuildingId);
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserId", appartment.ManagerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserId", appartment.OwnerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", appartment.StatusId);
            return View(appartment);
        }

        // POST: Appartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartmentId,StatusId,OwnerId,AptNumber,AptSize,AptRent,AptNumberOfRooms,AptDescription,BuildingId,ManagerId")] Appartment appartment)
        {
            if (id != appartment.ApartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppartmentExists(appartment.ApartmentId))
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
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", appartment.BuildingId);
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserId", appartment.ManagerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "UserId", "UserId", appartment.OwnerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", appartment.StatusId);
            return View(appartment);
        }

        // GET: Appartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appartment = await _context.Appartments
                .Include(a => a.Building)
                .Include(a => a.Manager)
                .Include(a => a.Owner)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (appartment == null)
            {
                return NotFound();
            }

            return View(appartment);
        }

        // POST: Appartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appartment = await _context.Appartments.FindAsync(id);
            if (appartment != null)
            {
                _context.Appartments.Remove(appartment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppartmentExists(int id)
        {
            return _context.Appartments.Any(e => e.ApartmentId == id);
        }
    }
}
