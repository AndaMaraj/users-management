using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.Entities;
using UsersManagement.Repository.IRepository;
using UsersManagement.Repository.Repository;
using UsersManagement.Services.DTO;
using UsersManagement.Services.IService;
using UsersManagement.Services.UOW;

namespace UsersManagement.Services.Service
{
    public class UserService : ServiceAsync<User, UserDto>, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IUniteOfWork uniteOfWork) : base(userRepository, mapper, uniteOfWork)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public new async Task<UserDto> GetByIdAsync(int id)
        {
            return mapper.Map<UserDto>(await userRepository.GetByIdAsync(id, src => src.Include(x => x.Role)));
        }
        /* public async Task<UserDto> UserDetailsByIdAsync(int id)
        {
            return mapper.Map<UserDto>(await userRepository.GetByIdAsync(id));
        }*/
        public async Task<List<UserDto>> GetAllUsersAsync()
        {

            return mapper.Map<List<UserDto>>(await userRepository.GetAll(include: src => src.Include(x => x.Role)));
        }
        public async Task<UserDto> GetByEmail(string email)
        {
            return mapper.Map<UserDto>(await userRepository.GetByEmail(email));
        }
    }
}
