using LogicaAplicacion.Dtos.ReportesDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUReportes
{
    public interface ICUHorarioMayorTurnos
    {
        HorarioMayorTurnosDTO Ejecutar(int anio, int mes);
    }
}
