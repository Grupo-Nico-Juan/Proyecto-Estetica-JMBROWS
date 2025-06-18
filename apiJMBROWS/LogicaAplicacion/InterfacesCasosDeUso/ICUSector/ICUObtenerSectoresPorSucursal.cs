using LogicaAplicacion.Dtos.SectorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUSector
{
    public interface ICUObtenerSectoresPorSucursal
    {
        IEnumerable<SectorDTOSuc> Ejecutar(int sucursalId);
    }
}
