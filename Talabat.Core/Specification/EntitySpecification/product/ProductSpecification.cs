using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
namespace Talabat.Core.Specification.EntitySpecification.product
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpicificationParams prodSpi)
            : base(p =>
                (string.IsNullOrEmpty(prodSpi.Search) || p.Name.Contains(prodSpi.Search))
                &&
                (!prodSpi.BrandId.HasValue || p.BrandId == prodSpi.BrandId)
                &&
                (!prodSpi.CategoryId.HasValue || p.CategoryId == prodSpi.CategoryId)
            )
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
            if (!string.IsNullOrEmpty(prodSpi.Sort))
            {
                switch (prodSpi.Sort)
                {
                    case "PriceAsc":
                        AddOrderByAsc(p => p.Price);
                        break;
                    case "PriceDes":
                        AddOrderByDes(p => p.Price);
                        break;
                    default:
                        AddOrderByAsc(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderByAsc(p => p.Name);
            }
            ApplayPagination((prodSpi.PageSize * (prodSpi.PageIndex - 1)), prodSpi.PageSize);
        }
        public ProductSpecification(int Id) : base(p => p.Id == Id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}