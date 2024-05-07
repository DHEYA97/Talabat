using System.ComponentModel.DataAnnotations;
using Talabat.Core.Models.Order;

namespace Talabat.APIs.Dtos
{
	public class OrderDtos
	{
		public string CartId { get; set; }
		public int DeliveryMethodId { get; set; }
		public AddressDto Address { get; set; }
	}
}
