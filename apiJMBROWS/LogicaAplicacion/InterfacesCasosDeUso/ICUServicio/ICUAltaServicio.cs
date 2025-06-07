using LogicaAplicacion.Dtos.ServicioDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUServicio
{
    public interface ICUAltaServicio
    {
        void Ejecutar(AltaServicioDTO dto);
    }
}