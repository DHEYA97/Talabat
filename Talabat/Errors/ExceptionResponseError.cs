namespace Talabat.APIs.Errors
{
	public class ExceptionResponseError : ApiResponseError
	{
		public string? Details { get; set; }
        public ExceptionResponseError(int statusCode, string? message = null,string? details = null) 
			: base(statusCode,message)
		{
			Details = details;
		}
	}
}