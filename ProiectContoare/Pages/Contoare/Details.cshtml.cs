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

namespace ProiectContoare.Pages.Contoare
{
    public class DetailsModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public DetailsModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }

        public Contor Contor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contor = await _context.Contor
                .Include(c => c.Consumator) // Include informațiile consumatorului
                .FirstOrDefaultAsync(m => m.ContorId == id);

            if (Contor == null)
            {
                return NotFound();
            }

            return Page();
        }

    }
}
