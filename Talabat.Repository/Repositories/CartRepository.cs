using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Repositories.Interfaces;

namespace Talabat.Repository.Repositories
{
	public class CartRepository : ICartRepository
	{
		private readonly IDatabase _database;
        public CartRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<bool> DeleteCartAsync(string CartId)
		{
			var flag = await _database.KeyDeleteAsync(CartId);
			return flag;
		}
		public async Task<CustomerCart?> GetCartAsync(string CartId)
		{
			//if(await _database.KeyExistsAsync(CartId))
			//{
			//	return null;
			//}
			var customerCart = await _database.StringGetAsync(CartId);
			return customerCart.IsNullOrEmpty ? null :  JsonSerializer.Deserialize<CustomerCart?>(customerCart!);
		}
		public async Task<CustomerCart?> UpdateCartAsync(CustomerCart customerCart)
		{
			var flag = await _database.StringSetAsync(customerCart.Id,JsonSerializer.Serialize(customerCart),TimeSpan.FromDays(1));
			if(flag is false) return null;
			return await GetCartAsync(customerCart.Id);
		}
	}
}