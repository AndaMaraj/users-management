using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.Entities;

namespace UsersManagement.Repository.IRepository
{
    public interface IUserRepository : IRepositoryAsync<User>
    {
        Task<User> GetByEmail(string email);
    }
}
