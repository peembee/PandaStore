﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PandaStore.Data;
using PandaStore.Models;
using Microsoft.AspNetCore.Identity;

namespace PandaStore.Controllers
{
    public class CampaignController : Controller
    {
        private readonly PandaStoreContext _context;
        private readonly UserManager<PandaUser> userManager;
        public CampaignController(PandaStoreContext context, UserManager<PandaUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
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
            var userId = userManager.GetUserId(User);
            var campaign = new Campaign
            {
                Id = userId,
                StartDate = DateTime.Today, // Sätt startdatum till dagens datum
                EndDate = DateTime.Today // Sätt slutdatum till dagens datum
            };

            // Skapa en SelectList med en enda item som representerar den inloggades ID
            var idList = new List<SelectListItem> { new SelectListItem { Value = userId, Text = userId } };

            // Tilldela SelectList till ViewBag.Id
            ViewBag.Id = new SelectList(idList, "Value", "Text");

            ViewData["FK_ProductID"] = new SelectList(_context.Products, "ProductID", "ProductTitel");
            return View(campaign);
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
            ViewData["FK_ProductID"] = new SelectList(_context.Products, "ProductID", "ProductTitel", campaign.FK_ProductID);
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
