using System.Collections.Generic;
using LogicaAplicacion.Dtos.ReportesDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUReportes
{
    public interface ICUIngresosSucursalSector
    {
        IEnumerable<IngresosSucursalDTO> Ejecutar(int anio, int mes);
    }
}
