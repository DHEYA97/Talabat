using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Service.Interfaces;
using Talabat.Core.Specification.EntitySpecification.Orders;
using Talabat.Core.UnitOfWork;

namespace Talabat.Service
{
	public class OrderService : IOrderService
	{
		private readonly ICartRepository _cartRepository;
		private readonly IUnitOfWork _unitOfWork;


		public OrderService(ICartRepository cartRepository, IUnitOfWork unitOfWork)
		{
			_cartRepository = cartRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Order?> CreateOrderAsync(string buyerEmail, string CartId, int DeliveryMethodId, Address address)
		{
			var cart = await _cartRepository.GetCartAsync(CartId);
			if(cart is null)
			{
				return null;
			}
			var OrderItems = new List<OrderItem>();
			if(cart?.cartItems?.Count > 0) { 
				foreach(var cartItem in cart.cartItems)
				{
					var product = await _unitOfWork.Repositories<Product>().GetByIdAsync(cartItem.Id);
					var productItem = new ProductItem(product!.Id, product.Name, product.PictureUrl);
					var orderItem = new OrderItem(productItem, product.Price, cartItem.Quantity);
					OrderItems.Add(orderItem);
				}
			}
			var DeliveryMethod = await _unitOfWork.Repositories<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
			
			var subTotal = OrderItems.Sum(item=>item.Price * item.Quantity);

			var order = new Order(buyerEmail,address,DeliveryMethod ?? new DeliveryMethod(),OrderItems,subTotal);

			await _unitOfWork.Repositories<Order>().AddAsync(order);

			var result = await _unitOfWork.CompleteAsync();
			if (result <= 0)
				return null;
			return order;
		}

		public async Task<IEnumerable<Order>> GetAllOrderAsync(string buyerEmail)
		{
			var orderSpecification = new OrderSpecification(buyerEmail);
			var orders = await _unitOfWork.Repositories<Order>().GetAllWithSpecificationAsync(orderSpecification);
			return orders;
		}

		public async Task<Order> GetSpicificOrderAsync(string buyerEmail, int orderId)
		{
			var orderSpecification = new OrderSpecification(buyerEmail, orderId);
			var order = await _unitOfWork.Repositories<Order>().GetByIdWithSpecificationAsync(orderSpecification);
			return order;
		}
		public async Task<IEnumerable<DeliveryMethod?>> GetAllDeliveryMethod()
		{
			return await _unitOfWork.Repositories<DeliveryMethod>().GetAllAsync();
		}
	}
}