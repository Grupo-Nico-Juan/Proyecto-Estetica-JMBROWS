using LogicaAplicacion.Dtos.TurnoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUTurno
{
    public interface ICUObtenerTurnoPorId
    {
        TurnoDTO Ejecutar(int id);
    }
}