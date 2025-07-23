using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.EF
{
    public class RepositorioSectores : IRepositorioSectores
    {
        private readonly EsteticaContext _context;

        public RepositorioSectores(EsteticaContext context)
        {
            _context = context;
        }

        public void Add(Sector obj)
        {
            obj.EsValido();
            _context.Sectores.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Sector> GetAll()
        {
            return _context.Sectores.ToList();
        }

        public Sector GetById(int id)
        {
            var sector = _context.Sectores.FirstOrDefault(s => s.Id == id);
            return sector ?? throw new Exception("Sector no encontrado");
        }

        public void Update(int id, Sector obj)
        {
            var original = GetById(id);
            if (original == null)
                throw new Exception("Sector no encontrado");

            _context.Entry(original).CurrentValues.SetValues(obj);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var obj = GetById(id);
            if (obj == null)
                throw new Exception("Sector no encontrado");

            _context.Sectores.Remove(obj);
            _context.SaveChanges();
        }

        public void Remove(Sector obj)
        {
            _context.Sectores.Remove(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Sector> ObtenerPorSucursal(int idSucursal)
        {
            return _context.Sectores.Where(s => s.SucursalId == idSucursal).ToList();
        }

        public IEnumerable<Sector> ObtenerSectoresConServiciosPorSucursal(int sucursalId)
        {
            return _context.Sectores
                .Include(s => s.Servicios)
                    .ThenInclude(serv => serv.Extras)
                .Include(s => s.Servicios)
                    .ThenInclude(serv => serv.Imagenes)
                .Where(s => s.SucursalId == sucursalId)
                .ToList();
        }

    }

}
