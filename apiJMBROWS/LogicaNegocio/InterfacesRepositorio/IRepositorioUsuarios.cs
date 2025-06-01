using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;

public interface IRepositorioUsuarios : IRepositorio<Usuario>
{
    Usuario GetByEmail(string email);
    IEnumerable<Usuario> GetByRol(string rol);
    bool ExisteCorreoElectronico(string email);
    IEnumerable<Empleado> GetEmpleados();
    void AsignarHabilidad(int empleadoId, int habilidadId);
    void QuitarHabilidad(int empleadoId, int habilidadId);

}

