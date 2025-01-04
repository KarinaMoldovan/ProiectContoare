using System;
using System.Collections.Generic;
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
            ViewData["FacturaId"] = new SelectList(_context.Factura, "FacturaId", "FacturaId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Obține factura selectată
            var factura = _context.Factura.FirstOrDefault(f => f.FacturaId == Plata.FacturaId);
            if (factura == null)
            {
                ModelState.AddModelError(string.Empty, "Factura selectată nu există.");
                ViewData["FacturaId"] = new SelectList(_context.Factura, "FacturaId", "FacturaId");
                return Page();
            }
            // Setează DataPlatii ca fiind data curentă
            Plata.DataPlatii = DateTime.Now;
            // Calculează penalizarea
            var dataScadenta = factura.DataEmitere.AddDays(14); // Scadența este la 14 zile după înregistrare
            var zileRestante = (DateTime.Now - dataScadenta).Days;
            zileRestante = Math.Max(zileRestante, 0); // Penalizările sunt doar pentru zilele restante
            var penalizare = 0.01m * factura.Suma * zileRestante;
            // Setează SumaPlatita ca suma facturii + penalizarea
            Plata.SumaPlatita = factura.Suma + penalizare;
            // Adaugă plata în baza de date
            _context.Plata.Add(Plata);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
