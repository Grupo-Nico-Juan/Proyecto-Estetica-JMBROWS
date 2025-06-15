using LogicaAplicacion.Dtos.PeriodoLaboralDTO;

public interface ICUObtenerPeriodosLaboralesPorEmpleada
{
    IEnumerable<PeriodoLaboralDTO> Ejecutar(int empleadaId);
}
