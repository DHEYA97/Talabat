using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Reflection;
using Talabat.APIs.Errors;
using Talabat.APIs.Mapping;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Service.Interfaces;
using Talabat.Core.UnitOfWork;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;
using Talabat.Repository.UnitOfWork;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
	public static class ApplicationServiceExtension
	{
		public static IServiceCollection AddCustomService(this IServiceCollection Services, WebApplicationBuilder builder)
		{
			
			//Add Repositories Injection
			Services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));
			Services.AddScoped<IUnitOfWork,UnitOfWork>();
			Services.AddScoped<ICartRepository, CartRepository>();
			Services.AddScoped<IOrderService, OrderService>();
			Services.AddSingleton<ITokenService, TokenService>();

			//Add AutoMapper
			Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

			//Add Validation Error
			Services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var error = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
														 .SelectMany(p => p.Value.Errors)
														 .Select(p => p.ErrorMessage).ToArray();
					var validationResponseError = new ValidationResponseError()
					{
						Errore = error
					};

					return new BadRequestObjectResult(validationResponseError);
				};
			});

			//Add Redis
			Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			});		
			return Services;
		}
	}
}