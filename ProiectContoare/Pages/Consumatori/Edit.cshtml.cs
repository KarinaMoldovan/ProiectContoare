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

namespace ProiectContoare.Pages.Consumatori
{
    public class EditModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public EditModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Consumator Consumator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumator =  await _context.Consumator.FirstOrDefaultAsync(m => m.ConsumatorId == id);
            if (consumator == null)
            {
                return NotFound();
            }
            Consumator = consumator;
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

            _context.Attach(Consumator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumatorExists(Consumator.ConsumatorId))
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

        private bool ConsumatorExists(int id)
        {
            return _context.Consumator.Any(e => e.ConsumatorId == id);
        }
    }
}
