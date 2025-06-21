using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Persistence.DataContext
{
    public class ProductDbContext : DbContext
    {
        #region Ctor
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
        { }
        #endregion

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Will Call every configuration files in this Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
        }
    }
}
