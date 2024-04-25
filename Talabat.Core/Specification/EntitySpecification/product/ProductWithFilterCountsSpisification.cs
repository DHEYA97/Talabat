using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specification.EntitySpecification.product
{
	public class ProductWithFilterCountsSpisification : BaseSpecification<Product>
	{
        public ProductWithFilterCountsSpisification(ProductSpicificationParams prodSpi)
			  : base(p =>
				  (string.IsNullOrEmpty(prodSpi.Search) || p.Name.Contains(prodSpi.Search))
				  &&
				  (!prodSpi.BrandId.HasValue || p.BrandId == prodSpi.BrandId)
				  &&
				  (!prodSpi.CategoryId.HasValue || p.CategoryId == prodSpi.CategoryId)
			  )
		{
            
        }
    }
}
