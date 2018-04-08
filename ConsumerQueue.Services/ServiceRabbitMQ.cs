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
    public class ServiceRabbitMQ: IServiceRabbitMQ
    {
        private readonly ConnectionFactory _factory;

        public ServiceRabbitMQ(ConnectionFactory connection)
        {
            _factory = connection;
            SetConfig();
        }

        private void SetConfig()
        {
            _factory.UserName = "guest";
            _factory.Password = "guest";
            _factory.Protocol = Protocols.AMQP_0_9_1;
            _factory.HostName = "localhost";
            _factory.Port = AmqpTcpEndpoint.UseDefaultPort;
        }

        /// <summary>
        /// Obtem um item da fila
        /// </summary>
        /// <returns>Dados do armazenados de acesso</returns>
        public QueueEntry Pull()
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

                        var consumer = new EventingBasicConsumer(channel);
                        var result = (BasicGetResult)consumer.Model.BasicGet("coleta", true);
                        if(result != null)
                            return UtilConverter.ByteArrayToObject<QueueEntry>(result.Body);
                        return default(QueueEntry);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void AddToDatabase(QueueEntry entity)
        //{
        //    try
        //    {
        //        base.Add(entity);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
