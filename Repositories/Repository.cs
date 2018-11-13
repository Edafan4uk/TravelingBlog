using Contracts.Repositories;
using System.Linq;
using System.Collections.Generic;
using Entities;

namespace Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public RepositoryContext RepositoryContext { get; }
        public Repository(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public void Add(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            RepositoryContext.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return RepositoryContext.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> FindAll()
        {
            return this.RepositoryContext.Set<TEntity>();
        }

        public void Update(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            RepositoryContext.Set<TEntity>().UpdateRange(entities);
        }

        public void Remove(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            RepositoryContext.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return RepositoryContext.Set<TEntity>().SingleOrDefault<TEntity>(predicate);
        }
    }
}
