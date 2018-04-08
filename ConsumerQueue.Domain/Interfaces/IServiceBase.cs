using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsumerQueue.Domain.Interfaces
{
    public interface IServiceBase<T>: IDisposable where T : class
    {
        void Add(T item);
        T Get(object id);
        IQueryable<T> GetAll();
    }
}
