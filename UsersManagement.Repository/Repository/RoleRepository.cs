using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.Entities;
using UsersManagement.Repository.IRepository;

namespace UsersManagement.Repository.Repository
{
    public class RoleRepository : RepositoryAsync<Role>, IRoleRepository
    {
        public RoleRepository(UsersDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
