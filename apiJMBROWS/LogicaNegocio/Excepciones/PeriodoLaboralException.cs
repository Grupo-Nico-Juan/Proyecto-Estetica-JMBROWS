using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class PeriodoLaboralException : ExcepcionReglaDeNegocio
    {
        public PeriodoLaboralException() : base() { }
        public PeriodoLaboralException(string mensaje) : base(mensaje) { }
        public PeriodoLaboralException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }   
        
}
