using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Talabat.APIs.Errors;
using Talabat.APIs.Mapping;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;

namespace Talabat.APIs.Extensions
{
	public static class ApplicationServiceExtension
	{
		public static IServiceCollection AddCustomService(this IServiceCollection Services)
		{
			
			//Add Repositories Injection
			Services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));

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
			return Services;
		}
	}
}
