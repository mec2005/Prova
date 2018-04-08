using ConsumerQueue.Data;
using ConsumerQueue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsumerQueue.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T: class
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IRepository<T> _repository;

        public ServiceBase(IUnitOfWork unit, IRepository<T> repository)
        {
            unitOfWork = unit;
            _repository = repository;
        }
        
        public void Add(T item)
        {
            _repository.Add(item);
            unitOfWork.Save();
        }

        public T Get(object id)
        {
            return _repository.Get(id);
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
