using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioSucursales : IRepositorioSucursales
    {
        private readonly EsteticaContext _context;

        public RepositorioSucursales(EsteticaContext context)
        {
            _context = context;
        }

        public void Add(Sucursal obj)
        {
            obj.EsValido();
            _context.Sucursales.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Sucursal> GetAll()
        {
            return _context.Sucursales.ToList();
        }

        public Sucursal GetById(int id)
        {
            return _context.Sucursales.FirstOrDefault(s => s.Id == id);
        }

        public void Update(int id, Sucursal obj)
        {
            var original = GetById(id);
            if (original == null)
                throw new Exception("Sucursal no encontrada");
            _context.Entry(original).CurrentValues.SetValues(obj);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var obj = GetById(id);
            if (obj == null)
                throw new Exception("Sucursal no encontrada");
            _context.Sucursales.Remove(obj);
            _context.SaveChanges();
        }

        public void Remove(Sucursal obj)
        {
            _context.Sucursales.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Sucursal> BuscarPorNombre(string nombre)
        {
            return _context.Sucursales.Where(s => s.Nombre.Contains(nombre)).ToList();
        }
    }

}
