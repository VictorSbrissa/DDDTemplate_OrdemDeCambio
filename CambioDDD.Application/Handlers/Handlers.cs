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

    public class LogHandler :
        IHandler<OrdemCriadaEvent>,
        IHandler<OrdemLiquidadaEvent>,
        IHandler<OrdemCanceladaEvent>,
        IHandler<OrdemExpiradaEvent>
    {
        public void Handler(OrdemCriadaEvent evento)
        {
            Console.WriteLine($"[LOG] Ordem criada: {evento.OrdemId} - Cliente: {evento.NomeCliente}");
        }
        public void Handler(OrdemLiquidadaEvent evento)
        {
            Console.WriteLine($"[LOG] Ordem criada: {evento.OrdemId} - Valor: {evento.ValorLiquidado}");
        }
        public void Handler(OrdemCanceladaEvent evento)
        {
            Console.WriteLine($"[LOG] Ordem criada: {evento.OrdemId} - Data: {evento.AcontecidoEm}");
        }
        public void Handler(OrdemExpiradaEvent evento)
        {
            Console.WriteLine($"[LOG] Ordem criada: {evento.OrdemId} - Data: {evento.AcontecidoEm}");
        }
    }

    public class NotificacaoHandler : IHandler<OrdemLiquidadaEvent>
    {
        public void Handler(OrdemLiquidadaEvent evento)
        {
            Console.WriteLine($"[NOTIFICAÇÃO] Cliente informado da liquidação da ordem {evento.OrdemId}");
        }
    }
}
