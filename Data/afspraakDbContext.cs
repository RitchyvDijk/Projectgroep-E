using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class AfspraakDbContext : DbContext
{
    public DbSet<afspraakModel> afspraakModel { get; set; }

    public AfspraakDbContext(DbContextOptions<AfspraakDbContext> options)
        : base(options)
    {
    }

}
