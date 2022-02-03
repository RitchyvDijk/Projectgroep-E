using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace webapplication.Areas.Identity.Data
{
    public class GebruikerDbContext : IdentityDbContext<Gebruiker>
    {

        public DbSet<Client> Clients { get; set; }
        public DbSet<Hulpverlener> Hulpverleners { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Afspraak> afspraakModel { get; set; }

        public GebruikerDbContext(DbContextOptions<GebruikerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>()
                .HasOne(c => c.hulpverlener).WithMany(h => h.Clienten);
        }
    }
}
