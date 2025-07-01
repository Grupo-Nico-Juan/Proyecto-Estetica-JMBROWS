using LogicaAplicacion.Dtos.ReportesDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUReportes
{
    public interface ICUEstadoTurnos
    {
        EstadoTurnosDTO Ejecutar(int anio, int mes);
    }
}
