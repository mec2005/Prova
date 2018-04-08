using ConsumerQueue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viajanet.Domain.Interfaces
{
    public interface IServiceQueue
    {
        void AddToQueue(QueueEntry entry);
        void AddToQueueAsync(QueueEntry entry);
    }
}
