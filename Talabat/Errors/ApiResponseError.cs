
namespace Talabat.APIs.Errors
{
	public class ApiResponseError
	{

		public int StatusCode { get; set; }
        public string Message { get; set; }
		public ApiResponseError(int statusCode, string message = null)
		{
			StatusCode = statusCode;
			Message = message ?? DefaultApiResponseErrorMessage(statusCode);
		}

		private string? DefaultApiResponseErrorMessage(int statusCode)
		{
			return statusCode switch
			{
				400 => "BadRequest",
				401 => "No Authorized",
				404 => "Resourse Not Found",
				500 => "Server Error",
				_ => null
			};
		}
	}
}