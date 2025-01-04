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

namespace ProiectContoare.Pages.Facturi
{
    public class EditModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public EditModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Factura Factura { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura =  await _context.Factura.FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }
            Factura = factura;
           ViewData["ContorId"] = new SelectList(_context.Contor, "ContorId", "NumarSerie");
           ViewData["PlataId"] = new SelectList(_context.Set<Plata>(), "PlataId", "PlataId");
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

            _context.Attach(Factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(Factura.FacturaId))
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

        private bool FacturaExists(int id)
        {
            return _context.Factura.Any(e => e.FacturaId == id);
        }
    }
}
