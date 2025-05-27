using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebService.API.Data.Entity;

namespace WebService.API.Services.IServices
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid? id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> DeleteRangeByIdAsync(IEnumerable<Guid> entityIds);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> Exists(Guid id);
        Task<bool> IsAllExists(IEnumerable<TEntity> entities);
        Task<bool> IsAllExistsById(IEnumerable<Guid> entityIds);
        IList<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate, params string[] navigationProperties);
    }
}
