﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specification
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; } = null;
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAsc { get; set; } = null;
		public Expression<Func<T, object>> OrderByDes { get; set; } = null;
		public int? Skip { get; set ; }
		public int? Take { get; set; }
		public bool IsEnablePageSize { get; set; }

		public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> newCriteria)
        {
            Criteria = newCriteria;
        }
		public void AddOrderByAsc(Expression<Func<T, object>> orderByAsc)
		{
			OrderByAsc = orderByAsc;
		}
		public void AddOrderByDes(Expression<Func<T, object>> orderByDes)
		{
			OrderByDes = orderByDes;
		}
		public void ApplayPagination(int? skip ,int? take)
		{
			Skip = skip;
			Take = take;
			IsEnablePageSize = true;
		}
	}
}