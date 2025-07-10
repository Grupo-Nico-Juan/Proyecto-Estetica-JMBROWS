
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Identity;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios
{
    public class CUAltaUsuario : ICUAltaUsuario
    {
        public IRepositorioUsuarios RepoUsuario { get; set; }
        private readonly PasswordHasher<Administrador> _hasher;
        public CUAltaUsuario(IRepositorioUsuarios repo)
        {
            RepoUsuario = repo;
            _hasher = new PasswordHasher<Administrador>();
        }

        public void AltaUsuario(RegistroAdministradorDTO dto)
        {
            if (RepoUsuario.GetByEmail(dto.Email) != null)
                throw new UsuarioException("Ya existe un usuario con ese email.");

            Administrador nuevo;

            if (dto.TipoUsuario.ToLower() == "administrador")
            {
                nuevo = new Administrador
                {
                    Email = dto.Email,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    PasswordPlano = dto.PasswordPlano,
                    Password = "" // temporal, se reemplaza luego con el hash
                };
            }
            else
            {
                throw new UsuarioException("Tipo de usuario no válido.");
            }

            nuevo.EsValido();
            // 🔐 Hashear con el objeto completo (mejor que usar null)
            nuevo.Password = _hasher.HashPassword(nuevo, dto.PasswordPlano);
            RepoUsuario.Add(nuevo);
        }

        public void AltaUsuario(RegistroEmpleadoDTO dto)
        {
            
            Usuario nuevo;
            if (dto.TipoUsuario.ToLower() == "empleado")
            {
                nuevo = new Empleado
                {
                   
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Color = dto.Color,
                    Cargo = dto.Cargo
                    // TODO: Sucursal, Habilidades, Turnos se agregan en próximos pasos
                };
            }
            else
            {
                throw new UsuarioException("Tipo de usuario no válido.");
            }
            nuevo.EsValido();
            RepoUsuario.Add(nuevo);

        }
    }
}
