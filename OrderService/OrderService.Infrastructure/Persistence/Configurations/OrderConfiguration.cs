using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.ProductId)
                   .IsRequired();

            builder.Property(o => o.Quantity)
                   .IsRequired();

            builder.Property(o => o.Total)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.ClientId)
                   .IsRequired();

            builder.Property(o => o.OrderDate)
                   .IsRequired();

            builder.Property(o => o.LoggedInEmployeeId)
                   .IsRequired();
        }

    }
}
