using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Excepciones
{
    public class UsuarioException : ExcepcionReglaDeNegocio
    {
        public UsuarioException() { }

        public UsuarioException(string mensaje) : base(mensaje) { }

        public UsuarioException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
