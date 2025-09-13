using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Events
{
    public interface IDomainEvent
    {
        DateTime AcontecidoEm { get;}
    }
}
