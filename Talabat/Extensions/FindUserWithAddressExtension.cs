using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Models.Idintity;

namespace Talabat.APIs.Extensions
{
	public static class FindUserWithAddressExtension
	{
		public static async Task<ApplicationUser?> FirstOrDefaultUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			var getUser = await userManager.Users.Include(u=>u.Addres).FirstOrDefaultAsync(u => u.Email == userEmail);
			return getUser;
		}
	}
}
