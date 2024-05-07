using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.Core.Repositories.Interfaces
{
	public interface IGenericRepositories<T> where T : BaseEntity
	{
		Task<T?> GetByIdAsync(int Id);
		Task<IEnumerable<T?>> GetAllAsync();

		Task<T?> GetByIdWithSpecificationAsync(ISpecification<T> specification);
		Task<IEnumerable<T?>> GetAllWithSpecificationAsync(ISpecification<T> specification);
		Task<int> GetCountAsync(ISpecification<T> specification);
		Task AddAsync(T entity);
	}
}
