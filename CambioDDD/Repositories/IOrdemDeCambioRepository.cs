using CambioDDD.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Repositories
{
    public interface IOrdemDeCambioRepository
    {
        void Adicionar(OrdemDeCambio ordemDeCambio);
        OrdemDeCambio? ObterPorId(Guid idOrdem);
        IEnumerable<OrdemDeCambio> ObterTodas();
    }
}
