using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Repository.Specification
{
	public class SpecificationEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> Query,ISpecification<T> specification)
		{
			if(specification.Criteria is not null)
			{
				Query = Query.Where(specification.Criteria) ;
			}
			if (specification.OrderByAsc is not null)
			{
				Query = Query.OrderBy(specification.OrderByAsc);
			}
			if (specification.OrderByDes is not null)
			{
				Query = Query.OrderByDescending(specification.OrderByDes);
			}
			if(specification.IsEnablePageSize)
			{
				Query = Query.Skip(specification?.Skip ?? 0).Take(specification?.Take??10);
			}
			Query = specification.Includes.Aggregate(Query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
			return Query;
		}
	}
}