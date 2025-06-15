using LogicaAplicacion.Dtos.TurnoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno
{
    public interface ICUObtenerDetalleTurnoPorId
    {
        DetalleTurnoDTO Ejecutar(int id);
    }
}