using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Principal;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Mapping;
using Talabat.APIs.Middleware;
using Talabat.Core.Models.Idintity;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.DataSeed;
using Talabat.Repository.Repositories;

namespace Talabat
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			
			//Add EF DbContext
			builder.Services.AddDbContext<ApplicationDbContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			//Add Idintity DataBase
			builder.Services.AddDbContext<ApplicationIdintityDbContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			//Add Identity Service
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { }).AddEntityFrameworkStores<ApplicationIdintityDbContext>();

			// Add All Custom Service From Extention Method
			builder.Services.AddCustomService(builder);

			var app = builder.Build();
			
			//Add Migrate From Code 
			var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
			using var scope = scopeFactory.CreateScope();
			
			var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			var _IdintityContext = scope.ServiceProvider.GetRequiredService<ApplicationIdintityDbContext>();
			var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var _ILoggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

			try
			{
				//Context seeding
				await _context.Database.MigrateAsync();
				await DataSeed.SeedAsync(_context);

				//Idintity Seeding
				await _IdintityContext.Database.MigrateAsync();
				await RolesDataSeed.SeedRoleAsync(roleManger);
				await UserDataSeed.SeedUserAsync(userManger);
			}
			catch(Exception ex)
			{
				var _logger = _ILoggerFactory.CreateLogger<Program>();
				_logger.LogError(ex, "Error in Migration");
			}

			//add ExceptionMiddleware
			app.UseMiddleware<ExceptionMiddleware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStatusCodePagesWithReExecute("/ErroresNotFound/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
