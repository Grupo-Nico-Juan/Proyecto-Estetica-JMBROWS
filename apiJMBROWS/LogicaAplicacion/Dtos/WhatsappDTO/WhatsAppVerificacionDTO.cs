using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.WhatsappDTO
{
    public class WhatsAppVerificacionDTO
    {
        public required string TelefonoDestino { get; set; }  // en formato internacional ej: "+59891112222"
        public required string NombreCliente { get; set; }
        public required int ClienteId { get; set; } // para luego marcarlo como verificado
    }
}
