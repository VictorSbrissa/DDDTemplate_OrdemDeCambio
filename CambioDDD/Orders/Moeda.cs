using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Orders
{
    public class Moeda
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }

        public Moeda(string codigo, string nome) 
        {
            ValidaMoeda(codigo);

            Codigo = codigo.ToUpperInvariant();
            Nome = nome;        
        }
        public void ValidaMoeda(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentNullException("Código de moeda invalido");
            if (codigo.Length != 3)
                throw new ArgumentException("Código de moeda invalido");
        }

        public override bool Equals(object obj)
        {
            if (obj is Moeda outraMoeda)
            {
                return Codigo == outraMoeda.Codigo;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Codigo.GetHashCode();
        }

    }
}
