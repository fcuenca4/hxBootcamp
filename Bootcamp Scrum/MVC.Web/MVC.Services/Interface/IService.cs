using System;
using System.Collections.Generic;

namespace MVC.Services.Interface
{
    public interface IService<T>
    {
        T GetElement(Guid? id);
        IEnumerable<T> GetAllElements();
        T SaveElement(T element);
        T RemoveElement(T element);
        T UpdateElement(T element);
        T FindElement(Func<T, bool> p);
        ICollection<T> FindElements(Func<T, bool> p);
    }
}
