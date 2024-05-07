using Talabat.Core.Models.Order;

namespace Talabat.APIs.Dtos
{
	public class OrderToReturnDto
	{
		public int Id { get; set; }
		public string BayerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public string Status { get; set; }
		public Address Address { get; set; }
		public string DeliveryMethodName { get; set; }
		public decimal DeliveryMethodCost { get; set; }
		public ICollection<OrderItemDto> Items { get; set; }
		public decimal SubTotal { get; set; }
		public decimal Total {  get; set; }
		public string PaymentId { get; set; }
	}
}
