using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Interfaces;

namespace Talabat.Core.UnitOfWork
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepositories<TEntity> Repositories <TEntity>() where TEntity : BaseEntity;
		Task<int> CompleteAsync();
	}
}
