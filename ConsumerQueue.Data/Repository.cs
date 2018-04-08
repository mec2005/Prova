using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsumerQueue.Domain.Interfaces;

namespace ConsumerQueue.Data
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        protected readonly ColetaDataContext _context;
        
        public Repository(IUnitOfWork unit)
        {
            if (unit == null)
                throw new ArgumentNullException("UnitOfWork is null");

            this._context = unit as ColetaDataContext;
        }

        public virtual void Add(T item)
        {
            _context.Set<T>().Add(item);
        }
        
        public virtual T Get(object id)
        {
            return _context.Set<T>().Find(id);
        }
        
        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }        
    }
}
