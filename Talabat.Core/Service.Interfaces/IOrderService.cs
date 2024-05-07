using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order;

namespace Talabat.Core.Service.Interfaces
{
	public interface IOrderService
	{
		public Task<Order?> CreateOrderAsync(string buyerEmail, string CartId, int DeliveryMethodId, Address address);
		public Task<IEnumerable<Order>> GetAllOrderAsync(string buyerEmail);
		public Task<Order> GetSpicificOrderAsync(string buyerEmail, int orderId);
		public Task<IEnumerable<DeliveryMethod?>> GetAllDeliveryMethod();
	}
}
