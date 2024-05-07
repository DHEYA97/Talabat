using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Filter;
using Talabat.APIs.Hellper;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Specification;
using Talabat.Core.Specification.EntitySpecification;
using Talabat.Core.Specification.EntitySpecification.product;
using Talabat.Core.UnitOfWork;

namespace Talabat.APIs.Controllers
{
	[Jwt]
	public class ProductController : BaseApiController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[ProducesResponseType(typeof(Pagenation<ProductDTOs>),StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<ActionResult<Pagenation<ProductDTOs>>> GetProduct([FromQuery] ProductSpicificationParams prodSpicParams)
		{
			var productSpecification = new ProductSpecification(prodSpicParams);
			var product = await _unitOfWork.Repositories<Product>().GetAllWithSpecificationAsync(productSpecification);
			var productDto = _mapper.Map<IReadOnlyList<ProductDTOs>>(product);
			int count = await _unitOfWork.Repositories<Product>().GetCountAsync(new ProductWithFilterCountsSpisification(prodSpicParams));
			return Ok(new Pagenation<ProductDTOs>(prodSpicParams.PageSize, prodSpicParams.PageIndex, count, productDto));
		}

		[ProducesResponseType(typeof(ProductDTOs), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTOs>> GetProduct(int Id)
		{
			var product = await _unitOfWork.Repositories<Product>().GetByIdWithSpecificationAsync(new ProductSpecification(Id));
			if (product is null)
				return NotFound(new ApiResponseError(404,"Product Not Found"));

			var productDto = _mapper.Map<ProductDTOs>(product);
			return Ok(productDto);
		}
		[ProducesResponseType(typeof(IEnumerable<ProductBrand>), StatusCodes.Status200OK)]
		[HttpGet("brand")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _unitOfWork.Repositories<ProductBrand>().GetAllAsync();
			return Ok(brands);
		}
		[ProducesResponseType(typeof(IEnumerable<ProductBrand>), StatusCodes.Status200OK)]
		[HttpGet("category")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var category = await _unitOfWork.Repositories<ProductCategory>().GetAllAsync();
			return Ok(category);
		}
	}
}