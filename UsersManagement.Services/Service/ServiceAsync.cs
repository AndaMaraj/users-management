using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository.IRepository;
using UsersManagement.Services.IService;
using AutoMapper;
using UsersManagement.Services.DTO;
using UsersManagement.Repository.Entities;
using System.Linq.Expressions;

namespace UsersManagement.Services.Service
{
    public class ServiceAsync<TEntity, TDto> : IServiceAsync<TEntity, TDto> where TDto : BaseEntityDto where TEntity : BaseEntity
    {
        private readonly IRepositoryAsync<TEntity> _repository;
        private readonly IMapper _mapper;
        public ServiceAsync(IRepositoryAsync<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(TDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            await _repository.AddAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(await _repository.GetByIdAsync(id));
        }
        public async Task<IEnumerable<TDto>> GetAll(Expression<Func<TDto, bool>> expression = null)
        {
            var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
            var data = await _repository.GetAll(predicate);
            return data.Select(_mapper.Map<TDto>).ToList();
        }
        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }
        public async Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression)
        {
            var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
            var entity = _repository.GetFirstAsync(predicate);
            return _mapper.Map<TDto>(entity);
        }
    }
}
