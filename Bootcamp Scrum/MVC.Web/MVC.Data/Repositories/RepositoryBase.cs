using MVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MVC.Data.Repositories
{
    //empleo del tutorial
    //http://deviq.com/repository-pattern/
    public abstract class RepositoryBase<T>:IDisposable where T:EntityBase
    {
        private MoviesContext _dbContext;

        public RepositoryBase(MoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> List()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                   .Where(predicate)
                   .AsEnumerable();
        }

        public void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
        public T Find(Func<T, bool> p)
        {
            return _dbContext.Set<T>().Find(p);
        }
        public void Dispose()
        {
        }
    }
}
