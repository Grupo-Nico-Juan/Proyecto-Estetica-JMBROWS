using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class ServicioException : ExcepcionReglaDeNegocio
    {
        public ServicioException() : base() { }
        public ServicioException(string mensaje) : base(mensaje) { }
        public ServicioException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
