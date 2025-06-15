
using Libreria.LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Identity;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaNegocio.Entidades;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;

public class CULoginCliente : ICULoginCliente
{
    private readonly IRepositorioClientes _repo;
    private readonly PasswordHasher<Cliente> _hasher;

    public CULoginCliente(IRepositorioClientes repo)
    {
        _repo = repo;
        _hasher = new PasswordHasher<Cliente>();
    }

    public Cliente Ejecutar(LoginDTO dto)
    {
        var cliente = _repo.GetByEmail(dto.Email);
        if (cliente == null)
            throw new UsuarioException("Credenciales inválidas.");

        var resultado = _hasher.VerifyHashedPassword(cliente, cliente.Password, dto.Password);
        if (resultado == PasswordVerificationResult.Failed)
            throw new UsuarioException("Credenciales inválidas.");

        return cliente;
    }

}

