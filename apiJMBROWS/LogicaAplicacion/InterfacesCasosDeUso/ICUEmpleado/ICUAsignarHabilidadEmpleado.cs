using LogicaAplicacion.Dtos.EmpleadoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUAsignarHabilidadEmpleado
    {
        void Ejecutar(EmpleadoHabilidadDTO dto);
    }
}