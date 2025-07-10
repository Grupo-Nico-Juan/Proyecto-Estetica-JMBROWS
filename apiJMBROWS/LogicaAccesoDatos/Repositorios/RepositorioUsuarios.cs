using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using LogicaAccesoDatos.EF;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;

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
            return _context.Usuarios.OfType<Administrador>().Any(u => u.Email == email);
        }

        public Usuario GetByEmail(string email)
        {
            return _context.Usuarios
            .OfType<Administrador>()
            .FirstOrDefault(a => a.Email == email);
        }

        public IEnumerable<Usuario> GetByRol(string rol)
        {
            return _context.Usuarios.Where(u => u.Rol == rol).ToList();
        }

        public IEnumerable<Empleado> GetEmpleados()
        {
            return _context.Usuarios
            .OfType<Empleado>()
            .Include(e => e.PeriodosLaborales)
            .Include(e => e.TurnosAsignados)
            .Include(e => e.SectoresAsignados)
            .Include(e => e.Habilidades)
            .ToList();
        }
        public Empleado GetEmpleadoById(int id)
        {
            return _context.Usuarios
                .OfType<Empleado>()
                .Include(e => e.PeriodosLaborales)
                .Include(e => e.TurnosAsignados)
                .Include(e => e.SectoresAsignados)
                .Include(e => e.Habilidades)
                .FirstOrDefault(e => e.Id == id);
        }
        public void AddEmpleado(Empleado e)
        {
            e.EsValido();
            _context.Usuarios.Add(e);
            _context.SaveChanges();
        }
        public void UpdateEmpleado(int id, Empleado e)
        {
            var actual = GetEmpleadoById(id);
            if (actual == null)
                throw new EmpleadoException($"No se encontró el empleado con Id {id}");

            _context.Entry(actual).CurrentValues.SetValues(e);
            _context.SaveChanges();
        }
        public void DeleteEmpleado(int id)
        {
            var e = GetEmpleadoById(id);
            if (e == null)
                throw new EmpleadoException($"No se encontró el empleado con Id {id}");

            _context.Usuarios.Remove(e);
            _context.SaveChanges();
        }


        public void AsignarHabilidad(int empleadoId, int habilidadId)
        {
            var empleado = _context.Usuarios.OfType<Empleado>().Include(e => e.Habilidades).FirstOrDefault(e => e.Id == empleadoId)
                ?? throw new EmpleadoException("Empleado no encontrado");

            var habilidad = _context.Habilidades.FirstOrDefault(h => h.Id == habilidadId)
                ?? throw new HabilidadException("Habilidad no encontrada");

            if (!empleado.Habilidades.Contains(habilidad))
            {
                empleado.Habilidades.Add(habilidad);
                _context.SaveChanges();
            }
        }

        public void QuitarHabilidad(int empleadoId, int habilidadId)
        {
            var empleado = _context.Usuarios.OfType<Empleado>().Include(e => e.Habilidades).FirstOrDefault(e => e.Id == empleadoId)
                ?? throw new EmpleadoException("Empleado no encontrado");

            var habilidad = _context.Habilidades.FirstOrDefault(h => h.Id == habilidadId)
                ?? throw new HabilidadException("Habilidad no encontrada");

            if (empleado.Habilidades.Contains(habilidad))
            {
                empleado.Habilidades.Remove(habilidad);
                _context.SaveChanges();
            }
        }

        public void AsignarSector(int empleadoId, int sectorId)
        {
            var empleado = _context.Usuarios.OfType<Empleado>().Include(e => e.SectoresAsignados).FirstOrDefault(e => e.Id == empleadoId)
                 ?? throw new EmpleadoException("Empleado no encontrado");

            var sector = _context.Sectores.FirstOrDefault(h => h.Id == sectorId)
                ?? throw new Exception("Sector no encontrado");

            if (!empleado.SectoresAsignados.Contains(sector))
            {
                empleado.SectoresAsignados.Add(sector);
                _context.SaveChanges();
            }
        }

        public void QuitarSector(int empleadoId, int sectorId)
        {
            var empleado = _context.Usuarios.OfType<Empleado>().Include(e => e.SectoresAsignados).FirstOrDefault(e => e.Id == empleadoId)
                ?? throw new EmpleadoException("Empleado no encontrado");

            var sector = _context.Sectores.FirstOrDefault(h => h.Id == sectorId)
                ?? throw new Exception("Sector no encontrado");

            if (empleado.SectoresAsignados.Contains(sector))
            {
                empleado.SectoresAsignados.Remove(sector);
                _context.SaveChanges();
            }
        }
        



    }
}

