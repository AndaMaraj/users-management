using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.Entities;

namespace UsersManagement.Repository.IRepository
{
    public interface IRoleRepository : IRepositoryAsync<Role>
    {
        Task<Role> GetByNameAsync(string name);
    }
}
