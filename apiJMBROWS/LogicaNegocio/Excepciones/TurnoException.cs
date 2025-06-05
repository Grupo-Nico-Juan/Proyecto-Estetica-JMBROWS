using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class TurnoException : ExcepcionReglaDeNegocio
    {
        public TurnoException() : base() { }
        public TurnoException(string mensaje) : base(mensaje) { }
        public TurnoException(string mensaje, Exception inner) : base(mensaje, inner){ }
    }
}
