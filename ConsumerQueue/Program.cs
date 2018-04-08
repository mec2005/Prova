using ConsumerQueue.Data;
using ConsumerQueue.Domain.Interfaces;
using ConsumerQueue.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using SimpleInjector;
using System;
using System.Configuration;
using System.Threading;

namespace ConsumerQueue
{
    class Program
    {
        static readonly Container container;

        // Registra no container de IoC (poderia estar em outro projeto separado)
        static Program()
        {
            container = new Container();

            container.Register<IUnitOfWork>(() => {
                var db = new ColetaDataContext("cnn");
                db.Database.EnsureCreated();
                return db;                
            }, Lifestyle.Singleton);
            
            container.Register<IServiceRabbitMQ, ServiceRabbitMQ>(Lifestyle.Singleton);
            container.Register<IServiceDbQueue, ServiceDbQueue>(Lifestyle.Singleton);            

            container.RegisterSingleton(typeof(IRepository<>), typeof(Repository<>));
            container.Register<ConnectionFactory>(Lifestyle.Singleton);

            container.Verify();
        }

        static void Main(string[] args)
        {
            var serviceRabbitMQ = container.GetInstance<IServiceRabbitMQ>();

            Console.WriteLine("Precione ESC to sair");
            do
            {
                while (!Console.KeyAvailable)
                {
                    try
                    {
                        //Obtem da fila e salva na base de dados
                        var entry = serviceRabbitMQ.Pull();
                        if(entry != null)
                        {
                            var serviceDbQueue = container.GetInstance<IServiceDbQueue>();
                            // Adiciona a nova entrada na base de dados.
                            serviceDbQueue.AddToDatabase(entry);
                            Console.WriteLine("Novo item adicionado na base de dados!");
                        }
                        Thread.Sleep(5000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            
            Console.Read();
        }
    }
}
