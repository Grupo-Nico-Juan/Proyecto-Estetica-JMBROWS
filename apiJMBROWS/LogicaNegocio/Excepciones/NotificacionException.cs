using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class NotificacionException : ExcepcionReglaDeNegocio
    {
        public NotificacionException() : base() { }
        public NotificacionException(string mensaje) : base(mensaje) { }
        public NotificacionException(string mensaje, Exception inner) : base(mensaje, inner){ }
    }
}
