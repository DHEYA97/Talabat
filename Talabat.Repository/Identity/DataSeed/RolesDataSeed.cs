using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Const;

namespace Talabat.Repository.Identity.DataSeed
{
	public class RolesDataSeed
	{
		public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.Roles.Any())
			{
				await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
			}
		}
	}
}
