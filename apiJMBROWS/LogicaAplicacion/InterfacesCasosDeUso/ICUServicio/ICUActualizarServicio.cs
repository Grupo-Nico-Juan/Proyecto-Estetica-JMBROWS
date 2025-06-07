using LogicaAplicacion.Dtos.ServicioDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUServicio
{
    public interface ICUActualizarServicio
    {
        void Ejecutar(ActualizarServicioDTO dto);
    }
}