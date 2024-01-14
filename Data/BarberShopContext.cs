using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BarberShop.Models;

namespace BarberShop.Data
{
    public class BarberShopContext : DbContext
    {
        public BarberShopContext (DbContextOptions<BarberShopContext> options)
            : base(options)
        {
        }

        public DbSet<BarberShop.Models.Shop> Shop { get; set; } = default!;
        public DbSet<BarberShop.Models.Booking> Booking { get; set; } = default!;
        public DbSet<BarberShop.Models.Category> Category { get; set; } = default!;
        public DbSet<BarberShop.Models.Consumer> Consumer { get; set; } = default!;
        public DbSet<BarberShop.Models.AboutPage> About { get; set; } = default!;
    }
}
