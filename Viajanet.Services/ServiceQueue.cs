using ConsumerQueue.Domain.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viajanet.Domain.Interfaces;
using Viajanet.Services.Utilidades;

namespace Viajanet.Services
{
    public class ServiceQueue : IServiceQueue
    {
        private readonly IConnectionFactory _factory;

        public ServiceQueue()
        {
            try
            {
                _factory = new ConnectionFactory() { HostName = "localhost" };
            }
            catch (Exception)
            {
                //TODO: Logar erro
            }
        }

        public void AddToQueue(QueueEntry entry)
        {
            try
            {
                using (var connection = _factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "coleta",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                        

                        var body = UtilConverter.ObjectToByteArray<QueueEntry>(entry);

                        channel.BasicPublish(exchange: "",
                                     routingKey: "coleta",
                                     basicProperties: null,
                                     body: body);
                    }
                }
            }
            catch (Exception)
            {
                //TODO: Logar erro
            }
        }
        public async void AddToQueueAsync(QueueEntry entry)
        {
            await Task.Run(() => this.AddToQueue(entry));
        }
    }
}
