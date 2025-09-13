using CambioDDD.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using CambioDDD.Domain.Enums;
using static CambioDDD.Domain.Enums.EnumStatus;
using CambioDDD.Domain.Events;

namespace CambioDDD.Domain.Orders
{
    public class OrdemDeCambio
    {
        private readonly List<StatusHistorico> _historicoStatus = new();
        private readonly List<IDomainEvent> _eventos = new();

        public Guid OrderId { get; private set; }
        public Guid ClienteId { get; private set; }
        public Moeda MoedaOrigem { get; set; }
        public Moeda MoedaDestino { get; set; }
        public decimal ValorOperacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacaoStatus { get; set; }
        public EnumStatus.EnumStatusOrdem StatusAtual { get; private set; }
        public string NomeCliente { get; private set; }
        public IReadOnlyCollection<StatusHistorico> Historico => _historicoStatus.AsReadOnly();
        public IReadOnlyCollection<IDomainEvent> Eventos => _eventos.AsReadOnly();

        public OrdemDeCambio(Guid orderId, Guid clienteId, decimal valorOperacao, Moeda moedaOrigem, 
            Moeda moedaDestino, string nomeCliente)
        {
            ValidarOperacao(valorOperacao, moedaOrigem, moedaDestino, clienteId);

            OrderId = orderId;
            ClienteId = clienteId;
            NomeCliente = nomeCliente;
            ValorOperacao = valorOperacao;
            MoedaOrigem = moedaOrigem;
            MoedaDestino = moedaDestino;
            DataCriacao = DateTime.UtcNow;
            DataAtualizacaoStatus  = DateTime.UtcNow;
            StatusAtual = EnumStatusOrdem.Criada;
            StatusHistorico historico = new StatusHistorico();
            historico.DataMudanca = DateTime.UtcNow;
            historico.Status = EnumStatusOrdem.Criada;
            historico.Motivo = "Ordem criada";
            _historicoStatus.Add(historico);
            var eventoCriacao = new OrdemCriadaEvent(orderId, clienteId, valorOperacao, historico.Status, nomeCliente, moedaOrigem, moedaDestino);
            AddDomainEvent(eventoCriacao);
        }
        private void ValidarOperacao(decimal valorOperacao, Moeda moedaOrigem, Moeda moedaDestino, Guid clienteId)
        {
            if (valorOperacao <= 0)
            {
                throw new ArgumentException("Valor da operação deve ser maior que zero");
            }
            if (moedaOrigem.Equals(moedaDestino))
            {
                throw new ArgumentException("Valor de moeda origem nao pode ser igual ao destino");
            }
            if (clienteId == Guid.Empty)
            {
                throw new ArgumentException("Erro Guid cliente");
            }
        }

        public OrdemDeCambio Liquidar(Guid orderId, decimal valorOperacao)
        {
            if (StatusAtual != EnumStatusOrdem.Criada)
            {
                throw new ArgumentException("Ordem não pode ser liquidada neste status");
            }
            var agora = DateTime.UtcNow;
            StatusAtual = EnumStatusOrdem.Liquidada;
            DataAtualizacaoStatus = agora;
            RegistrarHistorico(EnumStatusOrdem.Liquidada, "Liquidada", agora);
            new OrdemLiquidadaEvent(orderId, valorOperacao);
            return this;
        }

        public OrdemDeCambio Cancelar() 
        {
            if (StatusAtual == EnumStatusOrdem.Criada)
            {
                var agora = DateTime.UtcNow;
                StatusAtual = EnumStatusOrdem.Cancelada;
                DataAtualizacaoStatus = agora;
                RegistrarHistorico(EnumStatusOrdem.Cancelada, "Cancelada", agora);
                return this;
            }
            return this;
        }
        public OrdemDeCambio Expirar()
        {
            if(StatusAtual == EnumStatusOrdem.Criada)
            {
                var agora = DateTime.UtcNow;
                StatusAtual = EnumStatusOrdem.Expirada;
                DataAtualizacaoStatus = agora;
                RegistrarHistorico(EnumStatusOrdem.Expirada, "Expirada", agora);
                return this;
            }
            return this;
        }

        private void RegistrarHistorico(EnumStatus.EnumStatusOrdem status, String motivo, DateTime agora)
        {
            StatusHistorico historico = new StatusHistorico();
            historico.DataMudanca = agora;
            historico.Status = status;
            historico.Motivo = motivo;
            _historicoStatus.Add(historico);
        }
        private void AddDomainEvent(IDomainEvent domainEvent)
        {
            _eventos.Add(domainEvent);
        }
    }
}
