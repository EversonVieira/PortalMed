using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User { Id = 1, UserName = "Admin", DisplayName = "Administrator", Email = "Admin@hotmail.com", Password = "Admin@123", RegisterDate = DateTime.Now }
                );

            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(10, 2);

            modelBuilder.Entity<Product>().HasData(
                    new Product { Id = 1, Name = "Notebook", Description = "Intel core i5, 8gb Ram, SSD 256hn, Screen 15,6 1920x1080", Price = 500, CreationDate = DateTime.Now }
                );
        }
    }
}
