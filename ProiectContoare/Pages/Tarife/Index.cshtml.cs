using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Tarife
{
    public class IndexModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public IndexModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }

        public IList<Tarif> Tarif { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Tarif = await _context.Tarif.ToListAsync();
        }
    }
}
