using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Reflection;
using Talabat.APIs.Errors;
using Talabat.APIs.Mapping;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Service.Interfaces;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;
using Talabat.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Talabat.APIs.Extensions
{
	public static class JwtServiceExtension
	{
		public static IServiceCollection AddJwtService(this IServiceCollection Services, WebApplicationBuilder builder)
		{
			//Add Default Schema
			Services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(option =>
			{
				option.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					ValidIssuer = builder.Configuration["JWT:Issuer"],
					ValidAudience = builder.Configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
				};
			});
			return Services;
		}
	}
}