using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Hellper;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Interfaces;

namespace Talabat.APIs.Controllers
{
	public class CartController : BaseApiController
	{
		private readonly ICartRepository _cartRepository;

		public CartController(ICartRepository cartRepository)
		{
			_cartRepository = cartRepository;
		}

		[ProducesResponseType(typeof(ApiResponse<CustomerCart?>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<ActionResult<CustomerCart>> GetCart([FromQuery]string CartId)
		{
			var cart = await _cartRepository.GetCartAsync(CartId);
			return Ok(new ApiResponse<CustomerCart?>("Success",cart ?? new CustomerCart(CartId), ""));
		}

		[ProducesResponseType(typeof(ApiResponse<CustomerCart?>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<ActionResult<CustomerCart>> UpdateCart(CustomerCart customerCart)
		{
			var cart = await _cartRepository.UpdateCartAsync(customerCart);
			if (cart is null)
				return BadRequest(new ApiResponseError(400));
			return Ok(new ApiResponse<CustomerCart?>("Success",cart, "Cart added successfully"));
		}
		[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
		[HttpDelete]
		public async Task<ActionResult> DeleteCart([FromQuery]string CartId)
		{
			var flag = await _cartRepository.DeleteCartAsync(CartId);
			return flag ? Ok(new ApiResponse<string>("Success","" ,"Cart Deleted successfully")) : NotFound(new ApiResponseError(404, "Cart Not Found"));
		}
	}
}