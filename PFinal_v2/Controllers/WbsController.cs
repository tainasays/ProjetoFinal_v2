﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFinal_v2.Data;
using PFinal_v2.Models;

namespace PFinal_v2.Controllers
{
    public class WbsController : Controller
    {
        private readonly PFinal_v2Context _context;

        public WbsController(PFinal_v2Context context)
        {
            _context = context;
        }

        // GET: Wbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wbs.ToListAsync());
        }

        // GET: Wbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wbs = await _context.Wbs
                .FirstOrDefaultAsync(m => m.WbsId == id);
            if (wbs == null)
            {
                return NotFound();
            }

            return View(wbs);
        }

        // GET: Wbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WbsId,Codigo,Descricao,IsChargeable")] Wbs wbs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wbs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wbs);
        }

        // GET: Wbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wbs = await _context.Wbs.FindAsync(id);
            if (wbs == null)
            {
                return NotFound();
            }
            return View(wbs);
        }

        // POST: Wbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WbsId,Codigo,Descricao,IsChargeable")] Wbs wbs)
        {
            if (id != wbs.WbsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wbs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WbsExists(wbs.WbsId))
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
            return View(wbs);
        }

        // GET: Wbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wbs = await _context.Wbs
                .FirstOrDefaultAsync(m => m.WbsId == id);
            if (wbs == null)
            {
                return NotFound();
            }

            return View(wbs);
        }

        // POST: Wbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wbs = await _context.Wbs.FindAsync(id);
            if (wbs != null)
            {
                _context.Wbs.Remove(wbs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WbsExists(int id)
        {
            return _context.Wbs.Any(e => e.WbsId == id);
        }
    }
}
