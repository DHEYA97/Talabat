using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Idintity;
using Talabat.Core.Service.Interfaces;

namespace Talabat.Service
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<string> GenerateToken(ApplicationUser user, UserManager<ApplicationUser> userManager)
		{
			// 01 - Header 


			// 02 Payload

			// 02-01 private Claim
			var AuthCliam = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName, user.DisplayName),
				new Claim(ClaimTypes.Email, user.Email),
			} ;
			var UserRoles = await userManager.GetRolesAsync(user);
			foreach(var Role in UserRoles)
			{
				AuthCliam.Add(new Claim(ClaimTypes.Role, Role));
			}

			//03 Signature
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

			//Generate Token
			var token = new JwtSecurityToken(
						issuer: _configuration["JWT:Issuer"],
						audience: _configuration["JWT:Audience"],
						expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:ExpDate"])),
						claims: AuthCliam,
						signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
					);

			//Return Token
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
