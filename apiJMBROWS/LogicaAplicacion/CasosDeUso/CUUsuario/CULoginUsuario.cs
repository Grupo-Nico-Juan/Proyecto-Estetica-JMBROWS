using Microsoft.AspNetCore.Identity;
using Libreria.LogicaNegocio.Excepciones;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaAplicacion.InterfacesCasosDeUso;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

public class CULoginUsuario : ICULoginUsuario
{
    private readonly IRepositorioUsuarios _repo;
    private readonly PasswordHasher<Usuario> _hasher;

    public CULoginUsuario(IRepositorioUsuarios repo)
    {
        _repo = repo;
        _hasher = new PasswordHasher<Usuario>();
    }

    public Usuario LoginUsuario(LoginDTO dto)
    {
        var usuario = _repo.GetByEmail(dto.Email);

        if (usuario == null)
            throw new UsuarioException("Email o contraseña incorrectos.");

        var resultado = _hasher.VerifyHashedPassword(usuario, usuario.Password, dto.Password);

        if (resultado != PasswordVerificationResult.Success)
            throw new UsuarioException("Email o contraseña incorrectos.");

        return usuario;
    }
}

