using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Events
{
    public class DomainEventDispatcher
    {
        private readonly List<object> _handlers = new();

        public void Register<TEvent>(IHandler<TEvent> handler) where TEvent : IDomainEvent
        {
            _handlers.Add(handler);
        }
        public void Dispatch(IDomainEvent evento)
        {
            foreach (var handler in _handlers)
            {
                var tipo = handler.GetType();
                foreach (var interfaceType in tipo.GetInterfaces())
                {
                    if (interfaceType.IsGenericType &&
                        interfaceType.GetGenericTypeDefinition() == typeof(IHandler<>) &&
                        interfaceType.GenericTypeArguments[0] == evento.GetType())
                    {
                        dynamic dynamicHandler = handler;
                        dynamicHandler.Handler((dynamic)evento);
                    }
                }
            }
        }
    }
}