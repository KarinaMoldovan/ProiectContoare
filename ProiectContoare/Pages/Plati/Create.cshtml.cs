using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Plati
{
    public class CreateModel : PageModel
    {
        private readonly ProiectContoareContext _context;

        public CreateModel(ProiectContoareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Plata Plata { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Obține facturile neplătite (PlataId este null sau 0)
            var facturiNeplatite = _context.Factura
                .Where(f => f.PlataId == null || f.PlataId == 0)
                .Select(f => new
                {
                    f.FacturaId,
                    Detalii = $"Factura #{f.FacturaId} - Suma: {f.Suma} RON"
                }).ToList();

            // Populează dropdown-ul
            ViewData["FacturaId"] = new SelectList(facturiNeplatite, "FacturaId", "Detalii");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reîncarcă facturile neplătite în caz de eroare
                var facturiNeplatite = _context.Factura
                    .Where(f => f.PlataId == null || f.PlataId == 0)
                    .Select(f => new
                    {
                        f.FacturaId,
                        Detalii = $"Factura #{f.FacturaId} - Suma: {f.Suma} RON"
                    }).ToList();
                ViewData["FacturaId"] = new SelectList(facturiNeplatite, "FacturaId", "Detalii");
                return Page();
            }

            // Obține factura selectată
            var factura = _context.Factura.FirstOrDefault(f => f.FacturaId == Plata.FacturaId);
            if (factura == null)
            {
                ModelState.AddModelError(string.Empty, "Factura selectată nu există.");
                return OnGet(); // Reîncarcă pagina
            }

            // Setează DataPlatii ca fiind data curentă
            Plata.DataPlatii = DateTime.Now;

            // Calculează penalizarea
            var dataScadenta = factura.DataEmitere.AddDays(14); // Scadența este la 14 zile după emitere
            var zileRestante = (DateTime.Now - dataScadenta).Days;
            zileRestante = Math.Max(zileRestante, 0); // Penalizările sunt doar pentru zilele restante
            var penalizare = 0.01m * factura.Suma * zileRestante;

            // Setează SumaPlatita ca suma facturii + penalizarea
            Plata.SumaPlatita = factura.Suma + penalizare;

            // Adaugă plata în baza de date
            _context.Plata.Add(Plata);
            await _context.SaveChangesAsync();

            // Actualizează coloana PlataId în tabelul Factura
            factura.PlataId = Plata.PlataId;
            _context.Factura.Update(factura);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
