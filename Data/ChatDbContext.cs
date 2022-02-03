using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ChatDbContext : DbContext
{
    public DbSet<PriveChat> PriveChat { get; set; }
    public DbSet<GroupChat> GroupChats { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Client> Clienten { get; set; }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }




}
