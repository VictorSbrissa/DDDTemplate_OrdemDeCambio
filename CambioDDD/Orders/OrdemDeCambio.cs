using CambioDDD.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Orders
{
    public class OrdemDeCambio
    {
        public enum EnumStatusAtual
        {
            Criada = 1,
            Liquidada = 2,
            Expirada = 3,
            Cancelada = 4
        }

        public Guid OrderId { get; private set; }
        public Guid ClienteId { get; private set; }
        public Moeda MoedaOrigem { get; set; }
        public Moeda MoedaDestino { get; set; }
        public decimal ValorOperacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacaoStatus { get; set; }
        public EnumStatusAtual StatusAtual { get; private set; }
        public string NomeCliente { get; private set; }


        public OrdemDeCambio(Guid orderId, Guid clienteId, decimal valorOperacao, Moeda moedaOrigem, Moeda moedaDestino, string nomeCliente)
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
            StatusAtual = EnumStatusAtual.Criada;
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

        private OrdemDeCambio Liquidar()
        {
            if (StatusAtual != EnumStatusAtual.Criada)
            {
                throw new ArgumentException("Ordem não pode ser liquidada neste status");
            }
            StatusAtual = EnumStatusAtual.Liquidada;
            DataAtualizacaoStatus = DateTime.UtcNow;
            return this;
        }

        private OrdemDeCambio Cancelar() 
        {
            if (StatusAtual == EnumStatusAtual.Criada)
            {
                StatusAtual = EnumStatusAtual.Cancelada;
                DataAtualizacaoStatus = DateTime.UtcNow;
            }
            return this;
        }
        private OrdemDeCambio expirar()
        {
            if(StatusAtual == EnumStatusAtual.Criada)
            {
                StatusAtual = EnumStatusAtual.Expirada;
                DataAtualizacaoStatus= DateTime.UtcNow; 
            }
            return this;
        }
    }
}
