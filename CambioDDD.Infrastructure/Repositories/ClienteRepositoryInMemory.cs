using CambioDDD.Domain.Client;
using CambioDDD.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Infrastructure.Repositories
{
    public class ClienteRepositoryInMemory : IClienteRepository
    {
        private readonly List<Cliente> _clienteRepository = new();
        public void Adicionar(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException("Criação invalida - cliente vazio.");
            if (_clienteRepository.Exists(x => x.ClientId.Equals(cliente.ClientId)))
                throw new ArgumentNullException("Criação invalida - Cliente já existe.");

            _clienteRepository.Add(cliente);
        }

        public Cliente? ObterPorId(Guid clienteId)
        {
            return _clienteRepository.FirstOrDefault(x => x.ClientId.Equals(clienteId));
        }

        public IEnumerable<Cliente> ObterTodos()
        {
            return _clienteRepository;
        }
    }
}
