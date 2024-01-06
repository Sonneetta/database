using Microsoft.EntityFrameworkCore;
using RGR.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGR
{
    public class ContextClass : DbContext
    {
        public DbSet<TEvent> Event { get; set; }
        public DbSet<TEventLocation> EventLocation { get; set; }
        public DbSet<TEventName> EventName { get; set; }
        public DbSet<TOwner> Owner { get; set; }
        public DbSet<TLocation> Location { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=BookingPlatform;Username=postgres;Password=011427223");
        }
    }
}
