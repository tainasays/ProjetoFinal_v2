using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFinal_v2.Data;
using PFinal_v2.Models;
using PFinal_v2.Models.ViewModels;

namespace PFinal_v2.Controllers
{
    public class DiasController : Controller
    {
        private readonly PFinal_v2Context _context;

        public DiasController(PFinal_v2Context context)
        {
            _context = context;
        }

     

        // GET: Dias --------------------- * * *
        public async Task<IActionResult> Index()
        {
            int usuarioId = int.Parse(User.FindFirst("UsuarioId").Value);
            var diasDoUsuario = await _context.Dia.Where(d => d.UsuarioId == usuarioId).ToListAsync();
            // TODO ------------ * * * * * * Mudar para Wbs's que estão contidas nos lançamentos recuperados na linha acima
            var wbsCadastrados = await _context.Wbs.ToListAsync();

            var dataAtual = DateTime.Today;
            List<Dia> diasDaQuinzena;

            if (dataAtual.Day <= 15)
            {
                diasDaQuinzena = diasDoUsuario.Where(d => d.DiaData.Day >= 1 && d.DiaData.Day <= 15 && d.DiaData.Month == dataAtual.Month && d.DiaData.Year == dataAtual.Year).ToList();
            }
            else
            {
                diasDaQuinzena = diasDoUsuario.Where(d => d.DiaData.Day > 15 && d.DiaData.Month == dataAtual.Month && d.DiaData.Year == dataAtual.Year).ToList();
            }

            var dias = new DiaFormViewModel()
            {
                Lancamentos = diasDaQuinzena,
                DataAtual = dataAtual,
                ListaWbs = wbsCadastrados,
            };

            return View(dias);
        }




        // GET: Dias/Details/5
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

        // GET: Dias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dias/Create
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

        // GET: Dias/Edit/5
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

        // POST: Dias/Edit/5
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

        // GET: Dias/Delete/5
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

        // POST: Dias/Delete/5
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
