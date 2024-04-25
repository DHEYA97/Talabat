using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.Specification;
using Talabat.Repository.Data;
using Talabat.Repository.Specification;

namespace Talabat.Repository.Repositories
{
	public class GenericRepositories<T> : IGenericRepositories<T> where T : BaseEntity
	{
		private readonly ApplicationDbContext _context;
		public GenericRepositories(ApplicationDbContext context)
		{
			_context = context;
		}
		private IQueryable<T> ApplaySpecification(ISpecification<T> specification)
		{
			return  SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specification);
		}
		public async Task<IEnumerable<T?>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}
		public async Task<T?> GetByIdAsync(int Id)
		{
			return await _context.Set<T>().FindAsync(Id);
		}
		public async Task<IEnumerable<T?>> GetAllWithSpecificationAsync(ISpecification<T> specification)
		{
			return await ApplaySpecification(specification).ToListAsync();
		}
		public async Task<T?> GetByIdWithSpecificationAsync(ISpecification<T> specification)
		{
			return await ApplaySpecification(specification).FirstOrDefaultAsync();
		}
		public async Task<int> GetCountAsync(ISpecification<T> specification)
		{
			return await ApplaySpecification(specification).CountAsync();
		}
	}
}