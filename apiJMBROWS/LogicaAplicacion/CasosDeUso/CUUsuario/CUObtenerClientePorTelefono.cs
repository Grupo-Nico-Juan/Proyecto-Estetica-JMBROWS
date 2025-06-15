using Libreria.LogicaNegocio.InterfacesRepositorio;

public class CUObtenerClientePorTelefono
{
    private readonly IRepositorioClientes _repo;
    public CUObtenerClientePorTelefono(IRepositorioClientes repo) { _repo = repo; }

    public ClienteDTO? Ejecutar(string telefono)
    {
        var cliente = _repo.GetByTelefono(telefono);
        if (cliente == null) return null;
        return new ClienteDTO
        {
            Id = cliente.Id,
            Telefono = cliente.Telefono,
            Email = cliente.Email,
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido
        };
    }
}