using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Idintity;

namespace Talabat.Core.Service.Interfaces
{
	public interface ITokenService
	{
		Task<string> GenerateToken(ApplicationUser user, UserManager<ApplicationUser> userManager);
	}
}
