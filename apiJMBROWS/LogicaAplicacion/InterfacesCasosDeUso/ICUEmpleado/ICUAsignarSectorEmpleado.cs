using LogicaAplicacion.Dtos.EmpleadoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUAsignarSectorEmpleado
    {
        void Ejecutar(EmpleadoSectorDTO dto);
    }
}