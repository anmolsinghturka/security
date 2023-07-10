using System;
using PicSecurityChecks.Shared;
using Microsoft.EntityFrameworkCore;

namespace PicSecurityChecks.api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }

        public DbSet<PIC_FlaggedNames> FlaggedNames { get; set; }
        public DbSet<PIC_SecurityCheckNames> SecurityCheckNames { get; set; }
        public DbSet<Inet> inets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inet>()
                .HasNoKey();
            base.OnModelCreating(modelBuilder); 
        }
    }
}
