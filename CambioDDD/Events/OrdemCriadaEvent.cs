using CambioDDD.Domain.Client;
using CambioDDD.Domain.Enums;
using CambioDDD.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Events
{
    public class OrdemCriadaEvent : IDomainEvent
    {  
        public Guid OrdemId { get;}
        public Guid ClienteId { get;}
        public Decimal ValorOperacao { get;}
        public EnumStatus.EnumStatusOrdem Status { get;}
        public DateTime AcontecidoEm { get; } = DateTime.UtcNow;
        public string NomeCliente { get;}
        public Moeda Origem { get;}
        public Moeda Destino { get;}

        public OrdemCriadaEvent(
            Guid ordemId, 
            Guid clienteId,
            decimal valorOperacao, 
            EnumStatus.EnumStatusOrdem status, 
            string nomeCliente, 
            Moeda origem, 
            Moeda destino
            )
        {
            OrdemId = ordemId;
            ClienteId = clienteId;
            ValorOperacao = valorOperacao;
            Status = status;
            NomeCliente = nomeCliente;
            Origem = origem;
            Destino = destino;
        }
    }
    public class OrdemLiquidadaEvent : IDomainEvent 
    {
        public Guid OrdemId { get;}
        public DateTime AcontecidoEm { get; } = DateTime.UtcNow;
        public decimal ValorLiquidado { get; }

        public OrdemLiquidadaEvent(Guid ordemId, decimal valorLiquidado)
        {
            OrdemId = ordemId;
            ValorLiquidado = valorLiquidado;
        }
    }

    public class OrdemCanceladaEvent : IDomainEvent 
    {
        public Guid OrdemId { get;}
        public DateTime AcontecidoEm { get;} = DateTime.UtcNow;
        public string Motivo { get;}
        public OrdemCanceladaEvent(Guid ordemId, string motivo)
        {
            OrdemId = ordemId;
            Motivo = motivo;
        }
    }

    public class OrdemExpiradaEvent : IDomainEvent 
    {
        public Guid OrdemId { get;}
        public DateTime AcontecidoEm { get;} = DateTime.UtcNow;

        public OrdemExpiradaEvent(Guid ordemId)
        {
            OrdemId = ordemId;
        }
    }
}
