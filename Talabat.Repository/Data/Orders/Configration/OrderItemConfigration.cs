using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order;

namespace Talabat.Repository.Data.Orders.Configration
{
	public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.OwnsOne(o => o.productItem, o => o.WithOwner());
			builder.Property(d => d.Price).HasColumnType("decimal(18,2)");
		}
	}
}
