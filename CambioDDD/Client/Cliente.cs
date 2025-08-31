using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Client
{
    public class Cliente
    {
        public Guid ClientId { get; private set; }
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }


        public Cliente(string nome, string documento, string email) 
        {
            ValidarCliente(nome, documento, email);

            ClientId = Guid.NewGuid();
            Nome = nome;
            Documento = documento;
            Email = email;
            DataCadastro = DateTime.UtcNow;
        }


        public void ValidarCliente(string nome, string documento, string email)
        {
            EmailAddressAttribute emailValido = new EmailAddressAttribute();
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException("Dados Invalidos - nome");
            if (string.IsNullOrEmpty(documento))
                throw new ArgumentNullException("Dados Invalidos - documento");
            if (!emailValido.IsValid(email))
                throw new ArgumentNullException("Dados Invalidos - email");
        }
    }
}
