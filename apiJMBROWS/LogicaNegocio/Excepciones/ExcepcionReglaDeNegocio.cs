﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class ExcepcionReglaDeNegocio : Exception
    {
        public ExcepcionReglaDeNegocio() { }

        public ExcepcionReglaDeNegocio(string mensaje) : base(mensaje) { }

        public ExcepcionReglaDeNegocio(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
