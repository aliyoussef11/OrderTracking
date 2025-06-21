using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Persistence.DataContext
{
    public class OrderDbContext : DbContext
    {
        #region Ctor
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
        { }
        #endregion

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Will Call every configuration files in this Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
        }
    }
}
