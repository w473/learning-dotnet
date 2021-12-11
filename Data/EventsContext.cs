using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Events.Models;

public class EventsContext : DbContext
{
    public EventsContext(DbContextOptions<EventsContext> options)
        : base(options)
    {
    }

    public DbSet<Events.Models.Event> Event { get; set; }

    public DbSet<Events.Models.Registration> Registration { get; set; }

    public DbSet<Events.Models.User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.GlobalId })
            .IsUnique(true);
    }
}
