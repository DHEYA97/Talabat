using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Filter;
using Talabat.Core.Models.Order;
using Talabat.Core.Service.Interfaces;

namespace Talabat.APIs.Controllers
{
	public class OrderController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}
		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
		[Jwt]
		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder(OrderDtos orderDtos)
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var MapAddress = _mapper.Map<Address>(orderDtos.Address);
			var order = await _orderService.CreateOrderAsync(BuyerEmail, orderDtos.CartId, orderDtos.DeliveryMethodId, MapAddress);
			if (order is null)
			{
				return BadRequest(new ApiResponseError(400, "There is Problem in Order parameter"));
			}
			return Ok(order);
		}
		[ProducesResponseType(typeof(IEnumerable<OrderToReturnDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
		[Jwt]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllUserOrder()
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var orders = await _orderService.GetAllOrderAsync(BuyerEmail);
			if (orders is null)
			{
				return NotFound(new ApiResponseError(404, "ther is no Order found"));
			}
			var orderDto = _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
			return Ok(orderDto);
		}

		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
		[Jwt]
		[HttpGet("{Id}")]
		public async Task<ActionResult<OrderToReturnDto>> GetUserOrderById(int Id)
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var order = await _orderService.GetSpicificOrderAsync(BuyerEmail,Id);
			if (order is null)
			{
				return NotFound(new ApiResponseError(404, $"ther is no Order with id {Id} found"));
			}
			var orderDto = _mapper.Map<OrderToReturnDto>(order);
			return Ok(orderDto);
		}
		[ProducesResponseType(typeof(IEnumerable<DeliveryMethod>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
		[HttpGet("GetDeliveryMethod")]
		public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethod()
		{
			var deliveryMethod = await _orderService.GetAllDeliveryMethod();
			if (deliveryMethod is null)
			{
				return NotFound(new ApiResponseError(404, $"ther is no Delivery Method with Found"));
			}
			return Ok(deliveryMethod);
		}
	}
}
