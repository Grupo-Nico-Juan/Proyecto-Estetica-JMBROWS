using LogicaAplicacion.Dtos.ExtraServicioDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUExtraServicio
{
    public interface ICUObtenerExtraServicioPorId
    {
        ExtraServicioDTO Ejecutar(int id);
    }
}
