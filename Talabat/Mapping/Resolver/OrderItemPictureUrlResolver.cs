using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;

namespace Talabat.APIs.Mapping.Resolver
{
	public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;
		public OrderItemPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.productItem.PicturUrl))
				return $"{_configuration["BaseUrl"]}{source.productItem.PicturUrl}";
			return string.Empty;
		}
	}
}
