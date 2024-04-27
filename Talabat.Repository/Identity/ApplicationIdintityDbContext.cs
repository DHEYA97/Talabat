using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Idintity;
using Talabat.Repository.Data;

namespace Talabat.Repository.Identity
{
	public class ApplicationIdintityDbContext : IdentityDbContext<ApplicationUser>
	{
        public ApplicationIdintityDbContext(DbContextOptions<ApplicationIdintityDbContext> options) : base(options)
		{
            
        }
    }
}
