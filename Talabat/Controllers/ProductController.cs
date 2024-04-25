using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Hellper;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Specification;
using Talabat.Core.Specification.EntitySpecification;
using Talabat.Core.Specification.EntitySpecification.product;

namespace Talabat.APIs.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepositories<Product> _ProductRepositories;
		private readonly IGenericRepositories<ProductBrand> _ProductBrandRepositories;
		private readonly IGenericRepositories<ProductCategory> _ProductCategoryRepositories;
		private readonly IMapper _mapper;
		public ProductController(IGenericRepositories<Product> productRepositories, IGenericRepositories<ProductBrand> productBrandRepositories, IGenericRepositories<ProductCategory> productCategoryRepositories, IMapper mapper)
		{
			_ProductRepositories = productRepositories;
			_ProductBrandRepositories = productBrandRepositories;
			_ProductCategoryRepositories = productCategoryRepositories;
			_mapper = mapper;
		}

		[ProducesResponseType(typeof(Pagenation<ProductDTOs>),StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<ActionResult<Pagenation<ProductDTOs>>> GetProduct([FromQuery] ProductSpicificationParams prodSpicParams)
		{
			var productSpecification = new ProductSpecification(prodSpicParams);
			var product = await _ProductRepositories.GetAllWithSpecificationAsync(productSpecification);
			var productDto = _mapper.Map<IReadOnlyList<ProductDTOs>>(product);
			int count = await _ProductRepositories.GetCountAsync(new ProductWithFilterCountsSpisification(prodSpicParams));
			return Ok(new Pagenation<ProductDTOs>(prodSpicParams.PageSize, prodSpicParams.PageIndex, count, productDto));
		}

		[ProducesResponseType(typeof(ProductDTOs), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTOs>> GetProduct(int Id)
		{
			var product = await _ProductRepositories.GetByIdWithSpecificationAsync(new ProductSpecification(Id));
			if (product is null)
				return NotFound(new ApiResponseError(404,"Product Not Found"));

			var productDto = _mapper.Map<ProductDTOs>(product);
			return Ok(productDto);
		}
		[ProducesResponseType(typeof(IEnumerable<ProductBrand>), StatusCodes.Status200OK)]
		[HttpGet("brand")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _ProductBrandRepositories.GetAllAsync();
			return Ok(brands);
		}
		[ProducesResponseType(typeof(IEnumerable<ProductBrand>), StatusCodes.Status200OK)]
		[HttpGet("category")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var category = await _ProductCategoryRepositories.GetAllAsync();
			return Ok(category);
		}
	}
}