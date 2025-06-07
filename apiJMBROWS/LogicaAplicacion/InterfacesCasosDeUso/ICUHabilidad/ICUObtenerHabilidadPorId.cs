using LogicaAplicacion.Dtos.HabilidadDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad
{
    public interface ICUObtenerHabilidadPorId
    {
        ActualizarHabilidadDTO Ejecutar(int id);
    }
}