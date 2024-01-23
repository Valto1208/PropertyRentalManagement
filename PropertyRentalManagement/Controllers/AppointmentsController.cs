using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyRentalManagement.Models;

namespace PropertyRentalManagement.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly FinalProjectDbContext _context;
        bool didSend;

        public AppointmentsController(FinalProjectDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var finalProjectDbContext = _context.Appointments.Include(a => a.Manager).Include(a => a.Status).Include(a => a.Tenant);
            return View(await finalProjectDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Manager)
                .Include(a => a.Status)
                .Include(a => a.Tenant)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            int userType = Convert.ToInt32(Request.Cookies["UserType"]);

            if (userType == 2)
            {              
                ViewData["TenantId"] = new SelectList(_context.Users.Where(m => m.UserType == 3), "UserId", "Name");
            }
            else if (userType == 3)
            {
                ViewData["TenantId"] = new SelectList(_context.Users.Where(m => m.UserType == 2), "UserId", "Name");
            }

            if (didSend == true)
            {
                ViewData["Message"] = "Appointment Scheduled!";
            }
            else
            {
                ViewData["Message"] = "";
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,TenantId,ManagerId,AppointmentDate,Location")] Appointment appointment)
        {

            int userId = Convert.ToInt32(Request.Cookies["UserId"]);
            int userType = Convert.ToInt32(Request.Cookies["UserType"]);

            if (userType == 2)
            {
                appointment.ManagerId = userId;
                ViewData["TenantId"] = new SelectList(_context.Users.Where(m => m.UserType == 3), "UserId", "Name", appointment.ManagerId);
            }
            else if (userType == 3)
            {
                appointment.TenantId = userId;
                ViewData["TenantId"] = new SelectList(_context.Users.Where(m => m.UserType == 2), "UserId", "Name", appointment.TenantId);
            }

            if (appointment.AppDateTime != null)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                didSend = true;
                return RedirectToAction(nameof(Index));
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserId", appointment.ManagerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", appointment.StatusId);
            ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "UserId", appointment.TenantId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,ManagerId,TenantId,AppDateTime,StatusId")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "UserId", appointment.ManagerId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", appointment.StatusId);
            ViewData["TenantId"] = new SelectList(_context.Users, "UserId", "UserId", appointment.TenantId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Manager)
                .Include(a => a.Status)
                .Include(a => a.Tenant)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
