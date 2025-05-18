using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;

public interface IRepositorioUsuarios : IRepositorio<Usuario>
{
    Usuario GetByEmail(string email);
    IEnumerable<Usuario> GetByRol(string rol);
    bool ExisteCorreoElectronico(string email);
}

