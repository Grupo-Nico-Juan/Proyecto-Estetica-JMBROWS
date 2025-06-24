using LogicaAplicacion.Dtos.ServicioDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUServicio
{
    public interface ICUAsignarHabilidadAServicio
    {
        void Ejecutar(HabilidadAServicioDTO dto);
    }
}
