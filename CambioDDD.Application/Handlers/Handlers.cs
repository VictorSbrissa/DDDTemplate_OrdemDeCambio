using CambioDDD.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Application.Handlers
{
    public class OrdemCriadaHandler : IHandler<OrdemCriadaEvent>
    {
        public void Handler(OrdemCriadaEvent evento)
        {
            // Aqui poderia ser um envio de email, notificação, log, etc.
            Console.WriteLine($"[Handler] Ordem criada para cliente {evento.NomeCliente}, valor: {evento.ValorOperacao}");
        }
    }
}
