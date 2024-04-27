using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Idintity;
using Talabat.Core.Const;
namespace Talabat.Repository.Identity.DataSeed
{
	public static class UserDataSeed
	{
		public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
		{
			ApplicationUser admin = new()
			{
				UserName = "admin",
				Email = "admin@Talabat.com",
				DisplayName = "Admin",
				EmailConfirmed = true
			};

			var user = await userManager.FindByEmailAsync(admin.Email);

			if (user is null)
			{
				await userManager.CreateAsync(admin, "P@ssword123");
				await userManager.AddToRoleAsync(admin, AppRoles.Admin);
			}
		}
	}
}