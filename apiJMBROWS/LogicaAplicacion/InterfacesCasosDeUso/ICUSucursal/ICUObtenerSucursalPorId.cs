using LogicaAplicacion.Dtos.SucursalDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal
{
    public interface ICUObtenerSucursalPorId
    {
        SucursalDTO Ejecutar(int id);
    }
}