using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Talabat.APIs.Filter
{
	public class JwtAttribute : TypeFilterAttribute
	{
		public JwtAttribute() : base(typeof(JwtFilter))
		{
		}
	}
	public class JwtFilter : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new UnauthorizedResult();
				return;
			}
		}
	}
}
