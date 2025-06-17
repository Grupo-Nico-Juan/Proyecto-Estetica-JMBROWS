using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Identity;

public class CUAltaCliente : ICUAltaCliente
{
    private readonly IRepositorioClientes _repo;
    private readonly PasswordHasher<Cliente> _hasher;

    public CUAltaCliente(IRepositorioClientes repo)
    {
        _repo = repo;
        _hasher = new PasswordHasher<Cliente>();
    }

    public void AltaCliente(RegistroClienteDTO dto)
    {
        if (_repo.ExisteTelefono(dto.Telefono))
            throw new UsuarioException("Ya existe un cliente con ese teléfono.");

        var nuevo = new Cliente
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Email = dto.Email,
            Telefono = dto.Telefono,
            PasswordPlano = dto.PasswordPlano,
            EsRegistrado = true,
            Password = "" // se completa luego
        };

        // Hashear con el objeto real
        nuevo.EsValido();

        if (!string.IsNullOrWhiteSpace(dto.PasswordPlano))
            nuevo.Password = _hasher.HashPassword(nuevo, dto.PasswordPlano);

        _repo.Add(nuevo);
    }
}


