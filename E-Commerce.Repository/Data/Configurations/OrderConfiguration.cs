using E_Commerce.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasMany(order=>order.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
			builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");

			builder.OwnsOne(order => order.ShippingAddress, o => o.WithOwner());
		}
	}
}
