using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class PromocionException : ExcepcionReglaDeNegocio
    {
        public PromocionException() : base() { }
        public PromocionException(string mensaje) : base(mensaje) { }
        public PromocionException(string mensaje, Exception inner) : base(mensaje, inner){ }
        }
}
