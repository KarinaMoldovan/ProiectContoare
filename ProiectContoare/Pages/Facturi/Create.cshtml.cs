using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Facturi
{
    public class CreateModel : PageModel
    {
        private readonly ProiectContoareContext _context;

        public CreateModel(ProiectContoareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int ContorId { get; set; }

        [BindProperty]
        public decimal ValoareActuala { get; set; }

        public IActionResult OnGet()
        {

            // Populăm dropdown-ul cu contoarele disponibile
            ViewData["ContorId"] = new SelectList(_context.Contor, "ContorId", "NumarSerie");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ContorId"] = new SelectList(_context.Contor, "ContorId", "NumarSerie");
                return Page();
            }
            // Găsim contorul selectat
            var contor = await _context.Contor.FindAsync(ContorId);
            if (contor == null)
            {
                ModelState.AddModelError("", "Contorul selectat nu există.");
                return Page();
            }
            // Calculăm consumul
            decimal consum = ValoareActuala - contor.ValoareActuala;
            if (consum < 0)
            {
                ModelState.AddModelError("", "Citirea actuală nu poate fi mai mică decât valoarea anterioară.");
                return Page();
            }

           
            // Actualizăm valoarea actuală a contorului
            contor.ValoareActuala = ValoareActuala;
            // Obținem cel mai recent tarif
            var tarif = _context.Tarif.OrderByDescending(t => t.TarifId).FirstOrDefault();
            if (tarif == null)
            {
                ModelState.AddModelError("", "Nu există niciun tarif disponibil pentru calcul.");
                return Page();
            }
            // Calculăm suma facturii
            decimal sumaFactura = consum * tarif.PretPeMetruCub;
            // Creăm factura
            var factura = new Factura
            {
                DataEmitere = DateTime.Now,
                Suma = sumaFactura,
                ContorId = ContorId,
            };
            // Salvăm modificările în baza de date
            _context.Contor.Update(contor);
            _context.Factura.Add(factura);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
