using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Facturi
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ProiectContoareContext _context;

        public DeleteModel(ProiectContoareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Factura Factura { get; set; } = default!;

        public int? PlataId { get; set; } // ID-ul plății asociate

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Factura = await _context.Factura
                .Include(f => f.Contor) // Include relația cu Contor
                .Include(f => f.Tarif) // Include relația cu Tarif
                .Include(f => f.Plata) // Include relația cu Plata
                .FirstOrDefaultAsync(m => m.FacturaId == id);

            if (Factura == null)
            {
                return NotFound();
            }

            // Setează PlataId dacă Plata există
            PlataId = Factura.Plata?.PlataId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.FindAsync(id);
            if (factura != null)
            {
                Factura = factura;
                _context.Factura.Remove(Factura);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
