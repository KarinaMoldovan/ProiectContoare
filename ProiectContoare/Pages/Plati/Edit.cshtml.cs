using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Plati
{
    public class EditModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public EditModel(ProiectContoare.Data.ProiectContoareContext context)
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

            var plata = await _context.Plata.FirstOrDefaultAsync(m => m.PlataId == id);
            if (plata == null)
            {
                return NotFound();
            }
            Plata = plata;

            // Populare opțiuni dropdown pentru ModalitateDePlata
            //ViewData["ModalitateDePlataOptions"] = new SelectList(
            //    new List<string> { "Numerar", "Card Bancar", "Transfer Bancar" }
            //);

            ViewData["FacturaId"] = new SelectList(_context.Factura, "FacturaId", "FacturaId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Plata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlataExists(Plata.PlataId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlataExists(int id)
        {
            return _context.Plata.Any(e => e.PlataId == id);
        }
    }
}
