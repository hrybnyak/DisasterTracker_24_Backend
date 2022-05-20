using DisasterTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace DisasterTracker.DataServices.Repository
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected ApplicationDbContext _dbContext;
        protected DbSet<TEntity> _entities;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _entities = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity? GetById(Guid id)
        {
            return _entities.Find(id);
        }

        public virtual async Task Insert(TEntity entity, bool shouldSave = false)
        {
            entity.Id ??= Guid.NewGuid();
            entity.CreatedOn ??= DateTime.UtcNow;
            entity.ModifiedOn ??= DateTime.UtcNow;
            _entities.Add(entity);
            await SaveChangesIfNeeded(shouldSave);
        }

        public virtual async Task Delete(Guid id, bool shouldSave = false)
        {
            TEntity? entityToDelete = _entities.Find(id);
            await Delete(entityToDelete, shouldSave);

        }

        public virtual async Task Delete(TEntity entityToDelete, bool shouldSave = false)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _entities.Attach(entityToDelete);
            }
            _entities.Remove(entityToDelete);
            await SaveChangesIfNeeded(shouldSave);
        }

        public virtual async Task Update(TEntity entityToUpdate, bool shouldSave = false)
        {
            entityToUpdate.ModifiedOn ??= DateTime.UtcNow;
            _entities.Attach(entityToUpdate);
            var entry = _dbContext.Entry(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            await SaveChangesIfNeeded(shouldSave);
        }

        private async Task SaveChangesIfNeeded(bool shouldSave)
        {
            if (shouldSave)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
