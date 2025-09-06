using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Enums
{
    public class EnumStatus
    {
        public enum EnumStatusOrdem
        {
            Criada = 1,
            Liquidada = 2,
            Expirada = 3,
            Cancelada = 4
        }
    }
}
