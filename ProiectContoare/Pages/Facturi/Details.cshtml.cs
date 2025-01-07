using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Facturi
{
    public class DetailsModel : PageModel
    {
        private readonly ProiectContoareContext _context;

        public DetailsModel(ProiectContoareContext context)
        {
            _context = context;
        }

        public Factura Factura { get; set; } = default!;
        public int? PlataId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Factura = await _context.Factura
                .Include(f => f.Contor)
                .Include(f => f.Tarif)
                .Include(f => f.Plata)
                .FirstOrDefaultAsync(m => m.FacturaId == id);

            if (Factura == null)
            {
                return NotFound();
            }

          
            PlataId = Factura.Plata?.PlataId;

            return Page();
        }
    }
}
