using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.Entities;
using UsersManagement.Services.DTO;

namespace UsersManagement.Services.IService
{
    public interface IRoleService : IServiceAsync<Role, RoleDto>
    {
        Task<RoleDto> RoleDetailsById(int id);
        Task<List<RoleDto>> GetAllRolesAsync();
    }
}
