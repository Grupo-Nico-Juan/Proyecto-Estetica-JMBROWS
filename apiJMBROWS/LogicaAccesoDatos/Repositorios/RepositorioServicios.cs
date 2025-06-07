using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioServicios : IRepositorioServicios
    {
        private readonly EsteticaContext _context;

        public RepositorioServicios(EsteticaContext context)
        {
            _context = context;
        }

        public void Add(Servicio obj)
        {
            obj.EsValido();
            _context.Servicios.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Servicio> GetAll()
        {
            return _context.Servicios.ToList();
        }

        public Servicio GetById(int id)
        {
            return _context.Servicios.FirstOrDefault(s => s.Id == id);
        }

        public void Update(int id, Servicio obj)
        {
            var original = GetById(id);
            if (original == null)
                throw new Exception("Servicio no encontrado");
            _context.Entry(original).CurrentValues.SetValues(obj);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var obj = GetById(id);
            if (obj == null)
                throw new Exception("Servicio no encontrado");
            _context.Servicios.Remove(obj);
            _context.SaveChanges();
        }

        public void Remove(Servicio obj)
        {
            _context.Servicios.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Servicio> BuscarPorNombre(string texto)
        {
            return _context.Servicios.Where(s => s.Nombre.Contains(texto)).ToList();
        }
    }

}
