using ConsumerQueue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerQueue.Domain.Interfaces
{
    public interface IServiceRabbitMQ
    {
        /// <summary>
        /// Obtem um item da fila
        /// </summary>
        /// <returns>Dados do armazenados de acesso</returns>
        QueueEntry Pull();
    }
}
