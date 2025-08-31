using CambioDDD.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Application.Services
{
    public interface IOrdemDeCambioService
    {
        void Adicionar(Guid clienteId, decimal valorOperacao, Moeda moedaOrigem, Moeda moedaDestino, string nomeCliente);
        OrdemDeCambio? ObterPorId(Guid idOrdem);
        IEnumerable<OrdemDeCambio> ObterTodas();
    }
}
