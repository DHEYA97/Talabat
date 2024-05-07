using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models
{
	public class CustomerCart
	{
		public CustomerCart(string id)
		{
			Id = id;
			cartItems = new List<CartItem>();
		}

		public string Id {  get; set; }
		public IList<CartItem> cartItems { get; set; }
	}
}