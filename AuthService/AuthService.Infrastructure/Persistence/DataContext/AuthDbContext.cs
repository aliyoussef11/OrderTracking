using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AuthService.Infrastructure.Persistence.DataContext
{
    public class AuthDbContext : DbContext
    {
        #region Ctor
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
        { }
        #endregion
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Will Call every configuration files in this Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}
