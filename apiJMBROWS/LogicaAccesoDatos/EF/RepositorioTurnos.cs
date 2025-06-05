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
    public class RepositorioTurnos : IRepositorioTurnos
    {
        private readonly EsteticaContext _context;

        public RepositorioTurnos(EsteticaContext context)
        {
            _context = context;
        }
        public void Add(Turno obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            obj.EsValido();

            _context.Turnos.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Turno> GetAll()
        {
            return _context.Turnos
                           .Include(t => t.Detalles)
                           .Include(t => t.Empleada) 
                           .Include(t => t.Cliente)  
                           .ToList();
        }

        public Turno GetById(int id)
        {
            return _context.Turnos
                           .Include(t => t.Detalles)
                           .Include(t => t.Empleada)
                           .Include(t => t.Cliente)
                           .FirstOrDefault(t => t.Id == id);
        }

        public void Remove(int id)
        {
            var turnoEnBdd = _context.Turnos.Find(id);
            if (turnoEnBdd == null)
                throw new InvalidOperationException($"No existe un turno con Id = {id}.");

            _context.Turnos.Remove(turnoEnBdd);
            _context.SaveChanges();
        }

        public void Remove(Turno obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var existe = _context.Turnos.Any(t => t.Id == obj.Id);
            if (!existe)
                throw new InvalidOperationException($"No existe un turno con Id = {obj.Id}.");

            _context.Turnos.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(int id, Turno obj)
        {
            //falta hacer
        }

        public IEnumerable<Turno> BuscarPorEmpleada(int empleadaId)
        {
            return _context.Turnos
                           .Include(t => t.Detalles)
                           .Include(t => t.Cliente)
                           .Where(t => t.EmpleadaId == empleadaId && !t.Cancelado)
                           .ToList();
        }

        public IEnumerable<Turno> ObtenerTurnosDelDiaPorEmpleada(int empleadaId, DateTime dia)
        {
            return _context.Turnos
                           .Include(t => t.Detalles)
                           .Include(t => t.Cliente)
                           .Where(t => t.EmpleadaId == empleadaId
                                       && t.FechaHora.Date == dia.Date
                                       && !t.Cancelado
                                       && !t.Realizado)
                           .ToList();
        }

        public IEnumerable<Turno> ObtenerTurnosDelDia(DateTime dia)
        {
            return _context.Turnos
                           .Include(t => t.Detalles)
                           .Include(t => t.Cliente)
                           .Where(t => t.FechaHora.Date == dia.Date
                                       && !t.Cancelado
                                       && !t.Realizado)
                           .ToList();
        }

     
    }
}
