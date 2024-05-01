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
	public class DeliveryMethodConfigration : IEntityTypeConfiguration<DeliveryMethod>
	{
		public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			builder.Property(d => d.DeliveryAmount).HasColumnType("decimal(18,2)");
		}
	}
}
