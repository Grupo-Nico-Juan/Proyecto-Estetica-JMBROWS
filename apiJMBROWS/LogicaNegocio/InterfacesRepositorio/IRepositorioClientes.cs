using LogicaNegocio.Entidades;

namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioClientes : IRepositorio<Cliente>
    {
        Cliente GetByEmail(string email);
        bool ExisteCorreoElectronico(string email);
        Cliente? GetByTelefono(string telefono);
        bool ExisteTelefono(string telefono);

    }
}
