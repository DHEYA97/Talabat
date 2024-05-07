using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Interfaces;
using Talabat.Core.UnitOfWork;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;

namespace Talabat.Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _applicationDbContext;
		private readonly Hashtable _repositoriesHashSet;
		public UnitOfWork(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
			_repositoriesHashSet = new Hashtable();
		}
		public async Task<int> CompleteAsync()
		{
			return await _applicationDbContext.SaveChangesAsync();
		}
		public async ValueTask DisposeAsync()
		{
			await _applicationDbContext.DisposeAsync();
		}
		public IGenericRepositories<TEntity> Repositories<TEntity>() where TEntity : BaseEntity
		{
			var type = typeof(TEntity).Name;
			if(!_repositoriesHashSet.ContainsKey(type))
			{
				var repo = new GenericRepositories<TEntity>(_applicationDbContext);
				_repositoriesHashSet.Add(type, repo);
			}
			return _repositoriesHashSet[type] as IGenericRepositories<TEntity>;
		}
	}
}