using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
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
            return _context.Servicios
                .Include(s => s.Extras)
                .Include(s => s.Habilidades)
                .Include(s => s.Imagenes)
                .Include(s => s.Sectores)
                .ToList();
        }

        public Servicio GetById(int id)
        {
            return _context.Servicios
                .Include(s => s.Extras)
                .Include(s => s.Imagenes)
                .FirstOrDefault(s => s.Id == id);
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
        public void AsignarSector(int servicioId, int sectorId)
        {
            var servicio = _context.Servicios.Include(s => s.Sectores).FirstOrDefault(s => s.Id == servicioId)
                ?? throw new ServicioException("Servicio no encontrado");

            var sector = _context.Sectores.FirstOrDefault(s => s.Id == sectorId)
                ?? throw new ServicioException("Sector no encontrado");

            if (!servicio.Sectores.Contains(sector))
            {
                servicio.Sectores.Add(sector);
                _context.SaveChanges();
            }
        }

        public void QuitarSector(int servicioId, int sectorId)
        {
            var servicio = _context.Servicios.Include(s => s.Sectores).FirstOrDefault(s => s.Id == servicioId)
                ?? throw new ServicioException("Servicio no encontrado");

            var sector = _context.Sectores.FirstOrDefault(s => s.Id == sectorId)
                ?? throw new ServicioException("Sector no encontrado");

            if (servicio.Sectores.Contains(sector))
            {
                servicio.Sectores.Remove(sector);
                _context.SaveChanges();
            }
        }
        public void AsignarHabilidad(int servicioId, int habilidadId)
        {
            var servicio = _context.Servicios.Include(s => s.Habilidades).FirstOrDefault(s => s.Id == servicioId)
                ?? throw new ServicioException("Servicio no encontrado");

            var habilidad = _context.Habilidades.FirstOrDefault(h => h.Id == habilidadId)
                ?? throw new ServicioException("Habilidad no encontrada");

            if (!servicio.Habilidades.Contains(habilidad))
            {
                servicio.Habilidades.Add(habilidad);
                _context.SaveChanges();
            }
        }

        public void QuitarHabilidad(int servicioId, int habilidadId)
        {
            var servicio = _context.Servicios.Include(s => s.Habilidades).FirstOrDefault(s => s.Id == servicioId)
                ?? throw new ServicioException("Servicio no encontrado");

            var habilidad = _context.Habilidades.FirstOrDefault(h => h.Id == habilidadId)
                ?? throw new ServicioException("Habilidad no encontrada");

            if (servicio.Habilidades.Contains(habilidad))
            {
                servicio.Habilidades.Remove(habilidad);
                _context.SaveChanges();
            }
        }
        public List<Habilidad> ObtenerHabilidadesDeServicio(int servicioId)
        {
            var servicio = _context.Servicios
                .Include(s => s.Habilidades)
                .FirstOrDefault(s => s.Id == servicioId)
                ?? throw new Exception("Servicio no encontrado");

            return servicio.Habilidades;
        }

        public List<Sector> ObtenerSectoresDeServicio(int servicioId)
        {
            var servicio = _context.Servicios
                .Include(s => s.Sectores)
                .FirstOrDefault(s => s.Id == servicioId)
                ?? throw new Exception("Servicio no encontrado");

            return servicio.Sectores;
        }


        public List<Servicio> ObtenerPorIds(IEnumerable<int> ids)
        {
            return _context.Servicios
                .Include(s => s.Habilidades)
                .Where(s => ids.Contains(s.Id))
                .ToList();
        }

        public List<Servicio> GetServiciosPorSector(int sectorId)
        {
            return _context.Servicios
            .Include(s => s.Sectores)
            .Include(s => s.Habilidades)
            .Where(s => s.Sectores.Any(sector => sector.Id == sectorId))
            .ToList();
        }


    }

}
