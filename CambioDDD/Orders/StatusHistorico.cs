using CambioDDD.Domain.Client;
using CambioDDD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioDDD.Domain.Orders
{

    public class StatusHistorico
    {
        public EnumStatus.EnumStatusOrdem Status { get;  set; }
        public DateTime DataMudanca { get; set; }
        public String? Motivo { get; set; }
        public Cliente? Reponsavel { get; set; }

    }
}
