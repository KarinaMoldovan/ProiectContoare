using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Tarife
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ProiectContoareContext _context;

        public CreateModel(ProiectContoareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tarif Tarif { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Obține cel mai recent tarif activ
            var tarifActiv = _context.Tarif
                .Where(t => t.DataSfarsit == null)
                .OrderByDescending(t => t.DataInceput)
                .FirstOrDefault();
            // Actualizează data sfârșit a tarifului activ
            if (tarifActiv != null)
            {
                tarifActiv.DataSfarsit = DateTime.Now;
                _context.Tarif.Update(tarifActiv);
            }
            // Creează un nou tarif cu data început setată la data curentă
            Tarif.DataInceput = DateTime.Now;
            Tarif.DataSfarsit = null; // Noul tarif nu are sfârșit deocamdată
            _context.Tarif.Add(Tarif);
            // Salvează modificările în baza de date
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}