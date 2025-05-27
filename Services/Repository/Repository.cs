using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebService.API.Data;
using WebService.API.Data.Entity;
using WebService.API.Services.IServices;

namespace WebService.API.Services.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected WebAPIDb _context;
        private DbSet<TEntity> _dbSet;
        public Repository(WebAPIDb context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();

        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public  IList<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties)
        {
            List<TEntity> list;

            var query = _dbSet.AsQueryable();

            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            list = query.Where(predicate).ToList<TEntity>();
            return list;
        }
        public Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id).ExecuteDeleteAsync() > 0;
        }

        public Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeByIdAsync(IEnumerable<Guid> entityIds)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        { 
            var a = await _dbSet.ToListAsync<TEntity>();
            
            return a;
        }

        public async Task<TEntity> GetByIdAsync(Guid? id)
        {
            return await _dbSet.FindAsync(id.Value);
        }

        public Task<bool> IsAllExists(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAllExistsById(IEnumerable<Guid> entityIds)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var item = await _dbSet.FindAsync(entity.Id);

            if (item == null) return null;

            _dbSet.Entry(item).CurrentValues.SetValues(entity);

            return entity;
        }

        public Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
