using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioDetalleTurno : IRepositorioDetalleTurno
    {
        private readonly EsteticaContext _context;

        public RepositorioDetalleTurno(EsteticaContext context)
        {
            _context = context;
        }

        public void Add(DetalleTurno detalle)
        {
            _context.DetallesTurno.Add(detalle);
            _context.SaveChanges();
        }

        public void Update(int id, DetalleTurno detalle)
        {
            var existente = _context.DetallesTurno.Find(id);
            if (existente != null)
            {
                existente.ServicioId = detalle.ServicioId;
                existente.TurnoId = detalle.TurnoId;
                _context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            var detalle = _context.DetallesTurno.Find(id);
            if (detalle != null)
            {
                _context.DetallesTurno.Remove(detalle);
                _context.SaveChanges();
            }
        }

        public DetalleTurno GetById(int id)
        {
            return _context.DetallesTurno
                .AsNoTracking()
                .FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<DetalleTurno> GetAll()
        {
            return _context.DetallesTurno
                .AsNoTracking()
                .ToList();
        }
    }
}