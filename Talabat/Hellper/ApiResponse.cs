namespace Talabat.APIs.Hellper
{
	public class ApiResponse<T>
	{
		public ApiResponse(string status, T? data, string? message = null)
		{
			Status = status;
			Message = message;
			Data = data;
		}
		public ApiResponse(string status, string? message = null)
		{
			Status = status;
			Message = message;
		}
		public string Status {  get; set; }
		public string? Message { get; set; }
		public T? Data { get; set; }
	}
}