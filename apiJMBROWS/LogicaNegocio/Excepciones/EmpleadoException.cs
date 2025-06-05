using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class EmpleadoException : ExcepcionReglaDeNegocio
    {
        public EmpleadoException() : base() { }
        public EmpleadoException(string mensaje) : base(mensaje) { }
        public EmpleadoException(string mensaje, Exception inner) : base(mensaje, inner){ }
    }
}
