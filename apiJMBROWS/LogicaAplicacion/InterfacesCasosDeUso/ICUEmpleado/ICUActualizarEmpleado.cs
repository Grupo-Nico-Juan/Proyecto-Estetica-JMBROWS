using LogicaAplicacion.Dtos.EmpleadoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUActualizarEmpleado
    {
        void Ejecutar(ActualizarEmpleadoDTO dto);
    }
}