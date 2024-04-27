using System.ComponentModel.DataAnnotations;
using Talabat.Core.Const;

namespace Talabat.APIs.Dtos
{
	public class LogInDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[RegularExpression(RegularExpression.PasswordRX, ErrorMessage = Error.PasswordError)]
		public string Password { get; set; }
	}
}
