using LogicaAplicacion.Dtos.EmpleadoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUQuitarHabilidadEmpleado
    {
        void Ejecutar(EmpleadoHabilidadDTO dto);
    }
}