using LogicaAplicacion.Dtos.TurnoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUTurno
{
    public interface ICUActualizarTurno
    {
        void Ejecutar(ActualizarTurnoDTO dto);
    }
}