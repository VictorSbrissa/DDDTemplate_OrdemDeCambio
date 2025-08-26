using CambioDDD.Domain.Orders;
using CambioDDD.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Infrastructure.Repositories
{
    public class OrdemDeCambioRepositoryInMemory : IOrdemDeCambioRepository
    {
        private readonly List<OrdemDeCambio> _ordemDeCambioRepository = new();

        public void Adicionar(OrdemDeCambio ordemDeCambio)
        {
            if (ordemDeCambio == null)
                throw new ArgumentNullException("Erro - Ordem de cambio invalida.");
            if (_ordemDeCambioRepository.Exists(x => x.OrderId.Equals(ordemDeCambio.OrderId)))
                throw new ArgumentNullException("Erro - Ordem de cambio já existe.");

            _ordemDeCambioRepository.Add(ordemDeCambio);
        }

        public OrdemDeCambio ObterPorId(Guid idOrdem)
        {
            return _ordemDeCambioRepository.FirstOrDefault(x => x.OrderId.Equals(idOrdem));
        }

        public IEnumerable<OrdemDeCambio> ObterTodas()
        {
            return _ordemDeCambioRepository;
        }
    }
}
