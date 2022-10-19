using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.Entities;
using UsersManagement.Repository.IRepository;
using UsersManagement.Services.DTO;
using UsersManagement.Services.IService;
using UsersManagement.Services.UOW;

namespace UsersManagement.Services.Service
{
    public class RoleService : ServiceAsync<Role, RoleDto>, IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper, IUniteOfWork uniteOfWork) : base(roleRepository, mapper, uniteOfWork)
        {
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }
        public async Task<RoleDto> RoleDetailsById(int id)
        {
            return mapper.Map<RoleDto>(await roleRepository.GetByIdAsync(id));
        }
        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            return mapper.Map<List<RoleDto>>(await roleRepository.GetAll());
        }

        public async Task<RoleDto> GetByNameAsync(string name)
        {
            return mapper.Map<RoleDto>(await roleRepository.GetByNameAsync(name));
        }
    }
}
