using CambioDDD.Domain.Client;
using CambioDDD.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository) 
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void AdicionarCliente(string nome, string documento, string email)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(documento) || string.IsNullOrWhiteSpace(email))
                throw new Exception("Erro - criação invalida");

            Cliente cliente = new Cliente(
                nome,
                documento,
                email
                );

            _repository.Adicionar(cliente);
        }
        public Cliente ObeterPorId(Guid clienteId)
        {
            return _repository.ObterPorId(clienteId);
        }

        public IEnumerable<Cliente> ObterTodos()
        {
            return _repository.ObterTodos();
        }
    }
}
