using LogicaAplicacion.Dtos.ServicioDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUServicio
{
    public interface ICUObtenerServicioPorId
    {
        ServicioDTO Ejecutar(int id);
    }
}