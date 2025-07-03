using LogicaAccesoDatos.EF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioExtrasServicio : IRepositorioExtrasServicio
    {
        private readonly EsteticaContext _context;

        public RepositorioExtrasServicio(EsteticaContext context)
        {
            _context = context;
        }

        public void Add(ExtraServicio obj)
        {
            obj.EsValido();
            _context.ExtrasServicio.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<ExtraServicio> GetAll()
        {
            return _context.ExtrasServicio.Include(e => e.Servicio).ToList();
        }

        public ExtraServicio GetById(int id)
        {
            return _context.ExtrasServicio.Include(e => e.Servicio).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<ExtraServicio> ObtenerPorServicio(int servicioId)
        {
            return _context.ExtrasServicio.Where(e => e.ServicioId == servicioId).ToList();
        }

        public List<ExtraServicio> ObtenerPorIds(IEnumerable<int> ids)
        {
            return _context.ExtrasServicio.Where(e => ids.Contains(e.Id)).ToList();
        }

        public void Remove(int id)
        {
            var obj = _context.ExtrasServicio.Find(id);
            if (obj != null)
            {
                _context.ExtrasServicio.Remove(obj);
                _context.SaveChanges();
            }
        }

        public void Remove(ExtraServicio obj)
        {
            _context.ExtrasServicio.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(int id, ExtraServicio obj)
        {
            var existente = _context.ExtrasServicio.Find(id);
            if (existente == null) return;
            _context.Entry(existente).CurrentValues.SetValues(obj);
            _context.SaveChanges();
        }
    }
}
