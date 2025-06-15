using LogicaAplicacion.Dtos.EmpleadoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUQuitarSectorEmpleado
    {
        void Ejecutar(EmpleadoSectorDTO dto);
    }
}