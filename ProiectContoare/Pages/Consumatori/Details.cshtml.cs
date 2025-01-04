using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Consumatori
{
    public class DetailsModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public DetailsModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }

        public Consumator Consumator { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumator = await _context.Consumator.FirstOrDefaultAsync(m => m.ConsumatorId == id);
            if (consumator == null)
            {
                return NotFound();
            }
            else
            {
                Consumator = consumator;
            }
            return Page();
        }
    }
}
