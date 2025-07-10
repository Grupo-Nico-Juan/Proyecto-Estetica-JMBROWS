using Microsoft.AspNetCore.Identity;
using Libreria.LogicaNegocio.Excepciones;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaAplicacion.InterfacesCasosDeUso;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Libreria.LogicaNegocio.Entidades;

public class CULoginUsuario : ICULoginUsuario
{
    private readonly IRepositorioUsuarios _repo;
    private readonly PasswordHasher<Administrador> _hasher;

    public CULoginUsuario(IRepositorioUsuarios repo)
    {
        _repo = repo;
        _hasher = new PasswordHasher<Administrador>();
    }

    public Administrador LoginUsuario(LoginDTO dto)
    {
        Usuario usuarioBase = _repo.GetByEmail(dto.Email);

        if (usuarioBase is not Administrador usuario)
            throw new UsuarioException("Email o contraseña incorrectos.");

        var resultado = _hasher.VerifyHashedPassword(usuario, usuario.Password, dto.Password);

        if (resultado != PasswordVerificationResult.Success)
            throw new UsuarioException("Email o contraseña incorrectos.");

        return usuario;
    }
}

