using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Models;

namespace Talabat.APIs.Mapping.Resolver
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTOs, string>
	{
		private readonly IConfiguration _configuration;
		public ProductPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Product source, ProductDTOs destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
			return string.Empty;
		}
	}
}
