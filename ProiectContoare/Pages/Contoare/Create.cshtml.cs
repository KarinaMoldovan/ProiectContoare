using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Contoare
{
    public class CreateModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public CreateModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            ViewData["ConsumatorId"] = new SelectList(_context.Consumator, "ConsumatorId", "Email");
            return Page();
        }

        [BindProperty]
        public Contor Contor { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contor.Add(Contor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
