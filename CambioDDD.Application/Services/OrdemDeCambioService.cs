using CambioDDD.Domain.Client;
using CambioDDD.Domain.Orders;
using CambioDDD.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Application.Services
{
    public class OrdemDeCambioService : IOrdemDeCambioService
    {
        private readonly IOrdemDeCambioRepository _repository;

        public OrdemDeCambioService(IOrdemDeCambioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public void Adicionar(Guid clienteId, decimal valorOperacao, Moeda moedaOrigem, Moeda moedaDestino, string nomeCliente)
        {
            if (valorOperacao <= 0)
                throw new ArgumentException("Erro - Valor da ordem de câmbio deve ser maior que zero.");

            OrdemDeCambio ordemDeCambio = new OrdemDeCambio(
                Guid.NewGuid(),
                clienteId,
                valorOperacao,
                moedaOrigem,
                moedaDestino,
                nomeCliente
                );

            _repository.Adicionar(ordemDeCambio);
        }
        public OrdemDeCambio ObterPorId(Guid idOrdem)
        {
            return _repository.ObterPorId(idOrdem);
        }
        public IEnumerable<OrdemDeCambio> ObterTodas()
        {
            return _repository.ObterTodas();
        }
    }
}
