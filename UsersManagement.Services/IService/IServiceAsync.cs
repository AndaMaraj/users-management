using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Services.IService
{
    public interface IServiceAsync<TEntity, TDto>
    {
        Task AddAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TDto>> GetAll(Expression<Func<TDto, bool>> expression = null);
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression);
    }
}
