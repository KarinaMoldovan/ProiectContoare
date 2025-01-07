using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Plati
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
        public Plata Plata { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Plata = await _context.Plata
                .Include(p => p.Factura) 
                .FirstOrDefaultAsync(m => m.PlataId == id);

            if (Plata == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plata = await _context.Plata.FindAsync(id);
            if (plata != null)
            {
                Plata = plata;
                _context.Plata.Remove(Plata);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
