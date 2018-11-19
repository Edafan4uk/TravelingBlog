using TravelingBlog.BusinessLogicLayer.Contracts.Repositories;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TravelingBlog.DataAcceesLayer.Data;

namespace TravelingBlog.BusinessLogicLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public ApplicationDbContext ApplicationDbContext { get; }
        public DbSet<TEntity> Set { get; set; }
        public Repository(ApplicationDbContext applicationDbContext)
        {
            this.ApplicationDbContext = applicationDbContext;
            this.Set = ApplicationDbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            ApplicationDbContext.Set<TEntity>().Add(entity);
            ApplicationDbContext.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            ApplicationDbContext.Set<TEntity>().AddRange(entities);
            ApplicationDbContext.SaveChanges();
        }

        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return ApplicationDbContext.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> FindAll()
        {
            return this.ApplicationDbContext.Set<TEntity>();
        }

        public void Update(TEntity entity)
        {
            ApplicationDbContext.Set<TEntity>().Update(entity);
            ApplicationDbContext.SaveChanges();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            ApplicationDbContext.Set<TEntity>().UpdateRange(entities);
            ApplicationDbContext.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            ApplicationDbContext.Set<TEntity>().Remove(entity);
            ApplicationDbContext.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            ApplicationDbContext.Set<TEntity>().RemoveRange(entities);
            ApplicationDbContext.SaveChanges();
        }

        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return ApplicationDbContext.Set<TEntity>().SingleOrDefault<TEntity>(predicate);
        }
    }
}
