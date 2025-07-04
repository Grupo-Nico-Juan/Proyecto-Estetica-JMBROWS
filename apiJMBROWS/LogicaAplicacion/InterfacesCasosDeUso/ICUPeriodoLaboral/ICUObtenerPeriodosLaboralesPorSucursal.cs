using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using System;
using System.Collections.Generic;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral
{
    public interface ICUObtenerPeriodosLaboralesPorSucursal
    {
        Dictionary<DayOfWeek, List<PeriodoLaboralDTO>> Ejecutar(int sucursalId);
    }
}
