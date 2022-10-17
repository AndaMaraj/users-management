using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Repository;

namespace UsersManagement.Services.UOW
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly UsersDbContext _dbContext;
        public UniteOfWork(UsersDbContext usersDbContext)
        {
            this._dbContext = usersDbContext;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
