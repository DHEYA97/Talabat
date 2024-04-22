namespace Talabat.APIs.Errors
{
	public class ValidationResponseError : ApiResponseError
	{
        public IEnumerable<string> Errore { get; set; }
        public ValidationResponseError() : base(400)
		{

		}
	}
}