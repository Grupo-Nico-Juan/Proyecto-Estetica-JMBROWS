using System.Collections.Generic;
using LogicaAplicacion.Dtos.ReportesDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUReportes
{
    public interface ICUTurnosPorServicio
    {
        IEnumerable<TurnosPorServicioDTO> Ejecutar(int anio, int mes);
    }
}
