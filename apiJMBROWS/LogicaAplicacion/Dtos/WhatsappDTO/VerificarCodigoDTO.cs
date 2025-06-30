using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.WhatsappDTO
{
    public class VerificarCodigoDTO
    {
        public int ClienteId { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }

}
