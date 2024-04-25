using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Specification.EntitySpecification;
using Talabat.Core.Specification.EntitySpecification.product;

namespace Talabat.APIs.Controllers
{
	public class TestErrorController : BaseApiController
	{
		private readonly IGenericRepositories<Product> _ProductRepositories;

		public TestErrorController(IGenericRepositories<Product> productRepositories)
		{
			_ProductRepositories = productRepositories;
		}

		[HttpGet("NotFound")]
		public IActionResult NotFoundErrorTest()
		{
			return NotFound(new ApiResponseError (404));
		}
		[HttpGet("ValidationError/{id}")]
		public IActionResult ValidationErrorTest(int? id)
		{
			return Ok();
		}
		[HttpGet("UnauthorizedError")]
		public IActionResult UnauthorizedErrorTest()
		{
			return Unauthorized(new ApiResponseError(401));
		}
		[HttpGet("BadRequestError")]
		public IActionResult BadRequestErrorTest()
		{
			return BadRequest(new ApiResponseError(400));
		}
		[HttpGet("ServerExceptionError")]
		public async Task<IActionResult> ServerExceptionErrorTest()
		{
			var product = await _ProductRepositories.GetByIdWithSpecificationAsync(new ProductSpecification(500));
			var result = product.ToString();
			return Ok(result);
		}

	}
}