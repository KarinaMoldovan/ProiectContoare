using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Plati
{
    public class IndexModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public IndexModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }

        public IList<Plata> Plata { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Plata = await _context.Plata
                .Include(p => p.Factura).ToListAsync();
        }
    }
}
