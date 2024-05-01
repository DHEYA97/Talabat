using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order
{
	public class OrderItem : BaseEntity
	{
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItem productItem, decimal price, int quantity)
		{
			this.productItem = productItem;
			Price = price;
			Quantity = quantity;
		}

		public ProductItem productItem { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
