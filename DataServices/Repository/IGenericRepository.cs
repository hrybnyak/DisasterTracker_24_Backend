using DisasterTracker.Data;
using System.Linq.Expressions;

namespace DisasterTracker.DataServices.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");

        TEntity? GetById(Guid id);
        Task Insert(TEntity entity, bool shouldSave);
        Task Delete(Guid id, bool shouldSave);
        Task Delete(TEntity entityToDelete, bool shouldSave);
        Task Update(TEntity entityToUpdate, bool shouldSave);

    }
}
