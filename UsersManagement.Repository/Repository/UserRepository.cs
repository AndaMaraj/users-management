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
    public class UserRepository : RepositoryAsync<User>, IUserRepository
    {
        public UserRepository(UsersDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.Include(x => x.Role).ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
