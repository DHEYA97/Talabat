using System.ComponentModel.DataAnnotations;
using Talabat.Core.Const;

namespace Talabat.APIs.Dtos
{
	public class RegisterDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string DisplayName{ get; set; }
		[Required]
		[RegularExpression(RegularExpression.PhoneRX,ErrorMessage = Error.PhoneError)]
		public string PhoneNumber { get; set; }
		[Required]
		[RegularExpression(RegularExpression.PasswordRX,ErrorMessage = Error.PasswordError)]
		public string Password { get; set; }
		public List<string> Roles {  get; set; }

	}
}
