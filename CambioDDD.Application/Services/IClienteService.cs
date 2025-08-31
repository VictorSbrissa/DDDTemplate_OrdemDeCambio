using CambioDDD.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Application.Services
{
    public interface IClienteService
    {
        public void AdicionarCliente(string nome, string documento, string email);
        public Cliente ObeterPorId(Guid clienteId);
        public IEnumerable<Cliente> ObterTodos();
    }
}
