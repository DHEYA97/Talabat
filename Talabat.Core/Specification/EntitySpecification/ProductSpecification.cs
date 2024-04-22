using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
namespace Talabat.Core.Specification.EntitySpecification
{
	public class ProductSpecification : BaseSpecification<Product>
	{
        public ProductSpecification(string? sort) : base()
        {
            Includes.Add(p=>p.Brand);
			Includes.Add(p => p.Category);
			if(!string.IsNullOrEmpty(sort))
			{
				switch (sort)
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
		}
		public ProductSpecification(int Id) : base(p=>p.Id == Id)
		{
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
	}
}