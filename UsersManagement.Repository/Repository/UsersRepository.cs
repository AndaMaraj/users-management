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
    public class UsersRepository : RepositoryAsync<User>, IRepositoryAsync<User>
    {
        public UsersRepository(UsersDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.Include(x => x.Role).ToListAsync();
        }
    }
}
