using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PandaStore.Data;
using PandaStore.Models;

namespace PandaStore.Controllers
{
    public class CampaignController : Controller
    {
        private readonly PandaStoreContext _context;

        public CampaignController(PandaStoreContext context)
        {
            _context = context;
        }

        // GET: Campaign
        public async Task<IActionResult> Index()
        {
            var pandaStoreContext = _context.Campaigns.Include(c => c.PandaUsers).Include(c => c.Products);
            return View(await pandaStoreContext.ToListAsync());
        }

        // GET: Campaign/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Campaigns == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .Include(c => c.PandaUsers)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.CampaignID == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaign/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.PandaUsers, "Id", "Id");
            ViewData["FK_ProductID"] = new SelectList(_context.Products, "ProductID", "Description");
            return View();
        }

        // POST: Campaign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CampaignID,Id,FK_ProductID,StartDate,EndDate,Discount,IsActive")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.PandaUsers, "Id", "Id", campaign.Id);
            ViewData["FK_ProductID"] = new SelectList(_context.Products, "ProductID", "Description", campaign.FK_ProductID);
            return View(campaign);
        }

        // GET: Campaign/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Campaigns == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.PandaUsers, "Id", "Id", campaign.Id);
            ViewData["FK_ProductID"] = new SelectList(_context.Products, "ProductID", "Description", campaign.FK_ProductID);
            return View(campaign);
        }

        // POST: Campaign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CampaignID,Id,FK_ProductID,StartDate,EndDate,Discount,IsActive")] Campaign campaign)
        {
            if (id != campaign.CampaignID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.CampaignID))
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
            ViewData["Id"] = new SelectList(_context.PandaUsers, "Id", "Id", campaign.Id);
            ViewData["FK_ProductID"] = new SelectList(_context.Products, "ProductID", "Description", campaign.FK_ProductID);
            return View(campaign);
        }

        // GET: Campaign/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Campaigns == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns
                .Include(c => c.PandaUsers)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.CampaignID == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Campaigns == null)
            {
                return Problem("Entity set 'PandaStoreContext.Campaigns'  is null.");
            }
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign != null)
            {
                _context.Campaigns.Remove(campaign);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignExists(int id)
        {
          return (_context.Campaigns?.Any(e => e.CampaignID == id)).GetValueOrDefault();
        }
    }
}
