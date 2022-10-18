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
using UsersManagement.Services.UOW;

namespace UsersManagement.Services.Service
{
    public class ServiceAsync<TEntity, TDto> : IServiceAsync<TEntity, TDto> where TDto : BaseEntityDto where TEntity : BaseEntity
    {
        private readonly IRepositoryAsync<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IUniteOfWork _uniteOfWork;
        public ServiceAsync(IRepositoryAsync<TEntity> repository, IMapper mapper, IUniteOfWork uniteOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _uniteOfWork = uniteOfWork;
        }
        public async Task AddAsync(TDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            // todo: control if name that we are adding does not exit in db
            entity.CreatedOn = DateTime.UtcNow;
            await _repository.AddAsync(entity);
        }
        public async Task UpdateAsync(TDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            entity.UpdatedOn = DateTime.UtcNow;
            await _repository.UpdateAsync(entity);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _repository.GetByIdAsync(id);
            if (obj == null) return false;
            obj.IsDeleted = true;
            obj.DeletedOn = DateTime.UtcNow;
            await _uniteOfWork.SaveChangesAsync();
            return true;
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
