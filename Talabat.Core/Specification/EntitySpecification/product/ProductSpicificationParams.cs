using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification.EntitySpecification.product
{
	public class ProductSpicificationParams
	{

		private const int MaxPageSize = 10;
		private int? pageSize = 5;
		public int? PageSize
		{
			get { return pageSize; }
			set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
		}
		private string? search;
		public string? Search
		{
			get { return search; }
			set { search = value?.ToLower()??String.Empty; }
		}
		public string? Sort { get; set; }
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }
		public int PageIndex { get; set; } = 1;

	}
}
