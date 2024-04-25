using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Repositories.Interfaces
{
	public interface ICartRepository
	{
		Task<bool>DeleteCartAsync(string CartId);
		Task<CustomerCart?> GetCartAsync(string CartId);
		Task<CustomerCart?> UpdateCartAsync(CustomerCart customerCart);
	}
}
