using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Libreria.LogicaNegocio.Excepciones;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly EsteticaContext _context;

        public RepositorioUsuarios(EsteticaContext context)
        {
            _context = context;
        }

        public void Add(Usuario obj)
        {
            obj.EsValido();
            _context.Usuarios.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public void Remove(int id)
        {
            var usuario = GetById(id);
            if (usuario == null)
                throw new UsuarioException($"No se encontró el Usuario con Id {id}");

                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            
        }

        public void Remove(Usuario obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            _context.Usuarios.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(int id, Usuario obj)
        {
            Usuario u = GetById(id);
            if (u == null)
                throw new UsuarioException($"No se encontró el usuario con Id {id}");
            
            
                _context.Entry(u).CurrentValues.SetValues(obj);
                _context.SaveChanges();
            
        }

        public bool ExisteCorreoElectronico(string email)
        {
            return _context.Usuarios.Any(u => u.Email == email);
        }

        public Usuario GetByEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<Usuario> GetByRol(string rol)
        {
            return _context.Usuarios.Where(u => u.Rol == rol).ToList();
        }
    }
}

