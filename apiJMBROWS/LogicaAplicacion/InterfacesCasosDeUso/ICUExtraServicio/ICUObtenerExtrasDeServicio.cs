using LogicaAplicacion.Dtos.ExtraServicioDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUExtraServicio
{
    public interface ICUObtenerExtrasDeServicio
    {
        IEnumerable<ExtraServicioDTO> Ejecutar(int servicioId);
    }
}
