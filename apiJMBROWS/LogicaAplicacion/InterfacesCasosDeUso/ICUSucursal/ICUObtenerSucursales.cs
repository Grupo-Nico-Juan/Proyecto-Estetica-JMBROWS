using LogicaAplicacion.Dtos.SucursalDTO;
using System.Collections.Generic;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal
{
    public interface ICUObtenerSucursales
    {
        IEnumerable<SucursalDTO> Ejecutar();
    }
}