using LogicaAplicacion.Dtos.SucursalDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal
{
    public interface ICUAltaSucursal
    {
        void Ejecutar(AltaSucursalDTO dto);
    }

}
