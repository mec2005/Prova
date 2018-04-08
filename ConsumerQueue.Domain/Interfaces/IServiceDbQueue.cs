using ConsumerQueue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerQueue.Domain.Interfaces
{
    public interface IServiceDbQueue : IServiceBase<QueueEntry>
    {
        void AddToDatabase(QueueEntry entity);
    }
}
