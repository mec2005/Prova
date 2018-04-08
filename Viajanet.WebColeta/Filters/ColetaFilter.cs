using ConsumerQueue.Domain.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajanet.Domain;
using Viajanet.Domain.Interfaces;
using Viajanet.Services;

namespace Viajanet.WebColeta.Filters
{
    public class ColetaFilter : ActionFilterAttribute
    {
        private readonly IServiceQueue _serviceQueue;

        // TODO: Remover após implementação de IoC
        public ColetaFilter()
        {
            _serviceQueue = new ServiceQueue();
        }

        // Para injetar com Framework de IoC
        public ColetaFilter(IServiceQueue serviceQueue)
        {
            _serviceQueue = serviceQueue;
        }
        
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var ip = context.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();

            // Chama o serviço responsavel por gerenciar as gravações na fila (DDD - Services)
            _serviceQueue.AddToQueueAsync(new QueueEntry() {
                ActionName = context.RouteData.Values["action"].ToString(),
                ControllerName = context.RouteData.Values["controller"].ToString(),
                QueryString = context.HttpContext.Request.QueryString.Value,
                Dispositivo = context.HttpContext.Request.Headers["User-Agent"],
                Data = DateTime.Now,
                IP = ip                
            });

            base.OnActionExecuted(context);
        }        
    }
}
