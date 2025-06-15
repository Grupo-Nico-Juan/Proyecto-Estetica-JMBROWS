using LogicaAplicacion.Dtos.EmpleadoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUObtenerEmpleadoPorId
    {
        EmpleadoDTO Ejecutar(int id);
    }
}