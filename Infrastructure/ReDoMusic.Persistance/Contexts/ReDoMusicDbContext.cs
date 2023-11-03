using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RedoMusic.Persistence;
using ReDoMusic.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Instrument = ReDoMusic.Domain.Entites.Instrument;

namespace ReDoMusic.Persistance.Contexts
{
    public class ReDoMusicDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ReDoMusicDbContext(DbContextOptions<ReDoMusicDbContext> options) : base(options)
        {

        }
        public ReDoMusicDbContext() { }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configurations.GetString("ConnectionStrings:PostgreSQL"));
        }
    }
}
