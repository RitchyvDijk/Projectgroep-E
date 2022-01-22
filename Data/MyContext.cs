using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class MyContext : DbContext
    {
        public MyContext (DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        public DbSet<PriveChat> PriveChat { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Client> Clienten { get; set; }

        
    }
