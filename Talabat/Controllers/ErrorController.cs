using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
	[Route("ErroresNotFound/{statusCode}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErroresNotFoundController : ControllerBase
	{
		public IActionResult Error(int statusCode)
		{
			return NotFound(new ApiResponseError(statusCode,"EndPoint is Not Found"));
		}
	}
}
