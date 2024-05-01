using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order
{
	public class Order : BaseEntity
	{
        public Order()
        {
            
        }
        public Order(string bayerEmai, Address address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
		{
			BayerEmai = bayerEmai;
			Address = address;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
		}

		public string BayerEmai {  get; set; }
		public DateTimeOffset OrderDate {  get; set; } = DateTimeOffset.Now;
		public OrderStatus Status { get; set; } = OrderStatus.Pending;
		public Address Address { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }	
		public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();
		public decimal SubTotal { get; set; }
		public decimal Total()=> SubTotal + DeliveryMethod.DeliveryAmount;
		public string PaymentId { get; set; } = string.Empty;

	}
}