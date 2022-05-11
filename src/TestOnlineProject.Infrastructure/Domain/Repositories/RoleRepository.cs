using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.RoleAggregate;
using TestOnlineProject.Infrastructure.Database;

namespace TestOnlineProject.Infrastructure.Domain.Repositories
{
    public class RoleRepository : BaseRepository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {

        }
    }
}
