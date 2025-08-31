using CambioDDD.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Repositories
{
    public interface IClienteRepository
    {
        public void Adicionar(Cliente cliente);
        public Cliente? ObterPorId(Guid clienteId);
        public IEnumerable<Cliente> ObterTodos();
    }
}
