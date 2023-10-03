using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volxyseat.Domain.Core.Data;
using Volxyseat.Infrastructure.Data;

namespace Volxyseat.Infrastructure.Repository
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly ApplicationDataContext _applicationDataContext;
        protected readonly DbSet<TEntity> _entity;

        public IUnitOfWork UnitOfWork => _applicationDataContext;

        public RepositoryBase(ApplicationDataContext applicationDataContext)
        {
            _applicationDataContext = applicationDataContext;
            _entity = _applicationDataContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _entity.Update(entity);
        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _entity.Remove(entity);
            }
        }

        public void Dispose()
        {
            _applicationDataContext.Dispose();
        }
    }
}
