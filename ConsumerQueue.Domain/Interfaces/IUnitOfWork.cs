using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerQueue.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
