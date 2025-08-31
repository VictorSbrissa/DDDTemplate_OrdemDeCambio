using CambioDDD.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Orders
{
    public class OrdemDeCambio
    {
        public Guid OrderId { get; private set; }
        public decimal ValorOperacao { get; set; }
        public Moeda MoedaOrigem { get; set; }
        public Moeda MoedaDestino { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid ClienteId { get; private set; }

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
    }
}
