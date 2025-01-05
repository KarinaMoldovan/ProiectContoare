using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Tarife
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public DeleteModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tarif Tarif { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarif = await _context.Tarif.FirstOrDefaultAsync(m => m.TarifId == id);

            if (tarif == null)
            {
                return NotFound();
            }
            else
            {
                Tarif = tarif;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarif = await _context.Tarif.FindAsync(id);
            if (tarif != null)
            {
                Tarif = tarif;
                _context.Tarif.Remove(Tarif);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
