using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.InterfacesCasosDeUso;
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
        if (_repo.ExisteCorreoElectronico(dto.Email))
            throw new UsuarioException("Ya existe un cliente con ese correo electrónico.");

        var nuevo = new Cliente
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Email = dto.Email,
            PasswordPlano = dto.PasswordPlano,
            Password = "" // se completa luego
        };

        nuevo.EsValido();

        // Hashear con el objeto real
        nuevo.Password = _hasher.HashPassword(nuevo, dto.PasswordPlano);

        _repo.Add(nuevo);
    }
}


