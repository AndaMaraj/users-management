using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Models.Entities;
using UsersManagement.Repository.IRepository;

namespace UsersManagement.Repository.Repository
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
    {
        private readonly UsersDbContext _dbContext;
        public RepositoryAsync(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
             await _dbContext.Set<T>().AddAsync(entity);
        }
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            var result = _dbContext.Set<T>().AsNoTracking();
            if (expression != null)
                result = result.Where(expression);
            return result;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(expression);
        }

    }
}
