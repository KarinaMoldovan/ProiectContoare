using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Data;
using ProiectContoare.Models;

namespace ProiectContoare.Pages.Facturi
{
    public class IndexModel : PageModel
    {
        private readonly ProiectContoare.Data.ProiectContoareContext _context;

        public IndexModel(ProiectContoare.Data.ProiectContoareContext context)
        {
            _context = context;
        }

        public IList<Factura> Factura { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Factura = await _context.Factura
                .Include(f => f.Contor)
                .Include(f => f.Plata).ToListAsync();
        }
    }
}
