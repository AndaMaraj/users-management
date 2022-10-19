using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.Entities;
using UsersManagement.Services.DTO;

namespace UsersManagement.Services.IService
{
    public interface IUserService : IServiceAsync<User, UserDto>
    {
        //Task<UserDto> UserDetailsByIdAsync(int id);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetByEmail(string email);
    }
}
