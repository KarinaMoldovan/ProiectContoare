using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProiectContoare.Models;

namespace ProiectContoare.Data
{
    public class ProiectContoareContext : DbContext
    {
        public ProiectContoareContext (DbContextOptions<ProiectContoareContext> options)
            : base(options)
        {
        }

        public DbSet<ProiectContoare.Models.Consumator> Consumator { get; set; } = default!;
        public DbSet<ProiectContoare.Models.Contor> Contor { get; set; } = default!;
        public DbSet<ProiectContoare.Models.Factura> Factura { get; set; } = default!;
        public DbSet<ProiectContoare.Models.Plata> Plata { get; set; } = default!;
        public DbSet<ProiectContoare.Models.Tarif> Tarif { get; set; } = default!;
    }
}
