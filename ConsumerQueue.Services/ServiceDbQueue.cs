using ConsumerQueue.Domain.Interfaces;
using ConsumerQueue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ConsumerQueue.Services.Utilidades;

namespace ConsumerQueue.Services
{
    public class ServiceDbQueue : ServiceBase<QueueEntry>, IServiceDbQueue
    {
        private readonly ConnectionFactory _factory;

        public ServiceDbQueue(IUnitOfWork unit, IRepository<QueueEntry> repository)
            : base(unit, repository)
        {
        }

        public void AddToDatabase(QueueEntry entity)
        {
            try
            {
                base.Add(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
