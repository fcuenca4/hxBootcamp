using MVC.Data.Repositories;
using MVC.Entities;
using MVC.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MVC.Services
{
    public class ServiceProvider<T> : IService<T>, IDisposable where T : EntityBase
    {
        private RepositoryBase<T> repo;

        public ServiceProvider(RepositoryBase<T> repo)
        {
            this.repo = repo;
        }
        public T GetElement(Guid? id)
        {
            if (id.HasValue)
                return repo.GetById(id.Value);
            return null;
        }
        public IEnumerable<T> GetAllElements()
        {
            IEnumerable<T> toR = repo.List();
            if (toR.Count() > 0)
                return toR;
            return new List<T>();
        }
        public T SaveElement(T element)
        {
            if (element != null)
                repo.Insert(element);
            return element;
        }

        public T RemoveElement(T element)
        {
            if (element != null)
                repo.Delete(element);
            return element;
        }
        public T UpdateElement(T element)
        {
            if (element != null)
                repo.Update(element);
            return element;
        }

        public T FindElement(Func<T, bool> p)
        {
            return repo.Find(p);
        }

        public ICollection<T> FindElements(Func<T, bool> p)
        {
            var q = from c in repo.List()
                    where p.Invoke(c)
                    select c;
            return q.ToList();
        }

        public void Dispose()
        {
            repo.Dispose();
        }
    }
}
