using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class afspraakDbContext : DbContext
    {
        public afspraakDbContext (DbContextOptions<afspraakDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=afspraak.db");
        public DbSet<afspraakModel> afspraakModel { get; set; }
    }
