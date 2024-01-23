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
    public class MessagesController : Controller
    {
        private readonly FinalProjectDbContext _context;

        bool didSend = false;

        public MessagesController(FinalProjectDbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var finalProjectDbContext = _context.Messages.Where(m => m.ReceiverId == Convert.ToInt32(Request.Cookies["UserId"]));
            return View(await finalProjectDbContext.ToListAsync());
            
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
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
                ViewData["Message"] = "Message Sent!";
            }
            else
            {
                ViewData["Message"] = "";
            }
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,SenderId,ReceiverId,Content,DateTime")] Message message)
        {
            int userId = Convert.ToInt32(Request.Cookies["UserId"]);
            int userType = Convert.ToInt32(Request.Cookies["UserType"]);

            if (userType == 2)
            {
                message.SenderId = userId;
                ViewData["TenantId"] = new SelectList(_context.Users.Where(m => m.UserType == 3), "UserId", "Name", message.SenderId);
            }
            else if (userType == 3)
            {
                message.SenderId = userId;
                ViewData["TenantId"] = new SelectList(_context.Users.Where(m => m.UserType == 2), "UserId", "Name", message.SenderId);
            }
            if (ModelState.IsValid)
            {
                message.DateTime = DateTime.Now;
                _context.Add(message);
                didSend = true;
                await _context.SaveChangesAsync();
            }
            return Create();
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,SenderId,ReceiverId,Content,DateTime")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
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
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
