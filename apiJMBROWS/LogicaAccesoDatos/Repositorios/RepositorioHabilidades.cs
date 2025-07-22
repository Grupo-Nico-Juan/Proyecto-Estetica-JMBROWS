using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioHabilidades : IRepositorioHabilidades
    {
        private readonly EsteticaContext _context;

        public RepositorioHabilidades(EsteticaContext context)
        {
            _context = context;
        }

        public void Add(Habilidad obj)
        {
            obj.EsValido();
            _context.Habilidades.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Habilidad> GetAll()
        {
            return _context.Habilidades.ToList();
        }

        public Habilidad GetById(int id)
        {
            var habilidad = _context.Habilidades.FirstOrDefault(h => h.Id == id);
            return habilidad ?? throw new Exception("Habilidad no encontrada");
        }

        public void Update(int id, Habilidad obj)
        {
            var original = GetById(id);
            if (original == null)
                throw new Exception("Habilidad no encontrada");
            _context.Entry(original).CurrentValues.SetValues(obj);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var obj = GetById(id);
            if (obj == null)
                throw new Exception("Habilidad no encontrada");
            _context.Habilidades.Remove(obj);
            _context.SaveChanges();
        }

        public void Remove(Habilidad obj)
        {
            _context.Habilidades.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Habilidad> BuscarPorNombre(string texto)
        {
            return _context.Habilidades
                .Where(h => h.Nombre.Contains(texto))
                .ToList();
        }
    }
}

