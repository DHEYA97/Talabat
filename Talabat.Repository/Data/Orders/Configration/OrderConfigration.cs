using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order;

namespace Talabat.Repository.Data.Orders.Configration
{
	public class OrderConfigration : IEntityTypeConfiguration<Order>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
		{
			builder.Property(o => o.Status)
				   .HasConversion(s => s.ToString(), s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));
			
			builder.OwnsOne(o => o.Address,A=>A.WithOwner());

			builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);

			builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
		}
	}
}
