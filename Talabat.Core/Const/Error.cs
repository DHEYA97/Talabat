using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Const
{
	public static class Error
	{
		public const string PasswordError = @"At least one upper case English letter, (?=.*?[A - Z])
											  At least one lower case English letter, (?=.*?[a - z])
											  At least one digit, (?=.*?[0 - 9])
											  At least one special character, (?=.*?[#?!@$%^&*-])
											  Minimum eight in length.{8,}";

		public const string PhoneError = "Phone Must Be In Saudia Arabia Phone Like 0558795689";
	}
}
