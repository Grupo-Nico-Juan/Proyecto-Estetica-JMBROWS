
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios
{
    public class CUAltaUsuario : ICUAltaUsuario
    {
        public IRepositorioUsuarios RepoUsuario { get; set; }
        private readonly PasswordHasher<Usuario> _hasher;
        public CUAltaUsuario(IRepositorioUsuarios repo)
        {
            RepoUsuario = repo;
            _hasher = new PasswordHasher<Usuario>();
        }

        public void AltaUsuario(RegistroUsuarioDTO dto)
        {
            if (RepoUsuario.GetByEmail(dto.Email) != null)
                throw new UsuarioException("Ya existe un usuario con ese email.");

            Usuario nuevo;

            switch (dto.TipoUsuario.ToLower())
            {


                case "empleado":
                    nuevo = new Empleado
                    {
                        Email = dto.Email,
                        Nombre = dto.Nombre,
                        Apellido = dto.Apellido,
                        PasswordPlano = dto.PasswordPlano,
                        Password = "", // temporal, se reemplaza luego con el hash
                        Cargo = "Pendiente"
                    };
                    break;

                case "administrador":
                    nuevo = new Administrador
                    {
                        Email = dto.Email,
                        Nombre = dto.Nombre,
                        Apellido = dto.Apellido,
                        PasswordPlano = dto.PasswordPlano,
                        Password = "" // temporal, se reemplaza luego con el hash
                    };
                    break;

                default:
                    throw new UsuarioException("Tipo de usuario no válido.");
            }
            nuevo.EsValido();
            // 🔐 Hashear con el objeto completo (mejor que usar null)
            nuevo.Password = _hasher.HashPassword(nuevo, dto.PasswordPlano);
            RepoUsuario.Add(nuevo);
        }
    }
}
