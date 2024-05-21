using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFinal_v2.Data;
using PFinal_v2.Models;
using PFinal_v2.Models.ViewModels;

namespace PFinal_v2.Controllers
{
    public class FiltroDiasController : Controller
    {
        private readonly PFinal_v2Context _context;

        public FiltroDiasController(PFinal_v2Context context)
        {
            _context = context;
        }

        public IActionResult Index(string mes, int quinzena)
        {
            if (string.IsNullOrEmpty(mes))
            {
                mes = DateTime.Now.ToString("yyyy-MM");
            }

            DateTime startDate, endDate;
            DateTime.TryParse(mes + "-01", out startDate);

            if (quinzena == 1)
            {
                endDate = startDate.AddDays(14);
            }
            else
            {
                startDate = startDate.AddDays(15);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }

            var dias = _context.Dia
                .Where(d => d.DiaData >= startDate && d.DiaData <= endDate)
                .ToList();

            var viewModel = new DiaFormViewModel
            {
                DataAtual = DateTime.Now,
                ListaWbs = _context.Wbs.ToList(),
                Lancamentos = dias,
                Quinzena = quinzena,
                Mes = mes
            };

            return View(viewModel);
        }

        // GET: FiltroDias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dia = await _context.Dia
                .FirstOrDefaultAsync(m => m.DiaId == id);
            if (dia == null)
            {
                return NotFound();
            }

            return View(dia);
        }

        // GET: FiltroDias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FiltroDias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiaId,UsuarioId,WbsId,DiaData,Horas")] Dia dia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dia);
        }

        // GET: FiltroDias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dia = await _context.Dia.FindAsync(id);
            if (dia == null)
            {
                return NotFound();
            }
            return View(dia);
        }

        // POST: FiltroDias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiaId,UsuarioId,WbsId,DiaData,Horas")] Dia dia)
        {
            if (id != dia.DiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiaExists(dia.DiaId))
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
            return View(dia);
        }

        // GET: FiltroDias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dia = await _context.Dia
                .FirstOrDefaultAsync(m => m.DiaId == id);
            if (dia == null)
            {
                return NotFound();
            }

            return View(dia);
        }

        // POST: FiltroDias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dia = await _context.Dia.FindAsync(id);
            if (dia != null)
            {
                _context.Dia.Remove(dia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiaExists(int id)
        {
            return _context.Dia.Any(e => e.DiaId == id);
        }
    }
}
