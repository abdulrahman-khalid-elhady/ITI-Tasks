using Microsoft.EntityFrameworkCore;
using MVC_ITI_Project.Models;
using System.Linq.Expressions;

namespace MVC_ITI_Project.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly UniversityContext Context;
        protected readonly DbSet<TEntity> Set;

        public Repository(UniversityContext context)
        {
            Context = context;
            Set = Context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(object id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Set.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entity = await Set.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Set.AsQueryable();
            if (predicate != null) query = query.Where(predicate);
            foreach (var include in includes) query = query.Include(include);
            if (orderBy != null) query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Set.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Set.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            Set.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}


