using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Enums;
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
                    .ThenInclude(d => d.Servicio)
                .Include(t => t.Detalles)
                    .ThenInclude(d => d.Extras)
                        .ThenInclude(e => e.Servicio)
                .Include(t => t.Empleada)
                .Include(t => t.Cliente)
                .Include(t => t.Sucursal) 
                .ToList();
        }



        public Turno GetById(int id)
        {
            return _context.Turnos
                           .Include(t => t.Detalles)
                               .ThenInclude(d => d.Extras)
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
            var existente = _context.Turnos
                .Include(t => t.Detalles)
                .FirstOrDefault(t => t.Id == id);

            if (existente == null)
                throw new Exception("Turno no encontrado.");

            // Limpia los detalles existentes si necesitás reemplazarlos (opcional)
            // existente.Detalles.Clear(); 

            // Agrega los nuevos detalles del objeto entrante
            foreach (var detalle in obj.Detalles)
            {
                // Asegura que no se agreguen duplicados si estás agregando incrementalmente
                if (!existente.Detalles.Any(d => d.ServicioId == detalle.ServicioId))
                {
                    existente.Detalles.Add(new DetalleTurno
                    {
                        TurnoId = id,
                        ServicioId = detalle.ServicioId
                    });
                }
            }

            _context.SaveChanges();
        }


        public IEnumerable<Turno> BuscarPorEmpleada(int empleadaId)
        {
            return _context.Turnos
                           .Include(t => t.Detalles)
                               .ThenInclude(d => d.Extras)
                           .Include(t => t.Cliente)
                           .Where(t => t.EmpleadaId == empleadaId && t.Estado != EstadoTurno.Cancelado)
                           .ToList();
        }

        public IEnumerable<Turno> ObtenerTurnosDelDiaPorEmpleada(int empleadaId, DateTime dia)
        {
            return _context.Turnos
                .Include(t => t.Detalles)
                    .ThenInclude(d => d.Servicio)
                .Include(t => t.Detalles)
                    .ThenInclude(d => d.Extras)
                .Include(t => t.Cliente)
                .Where(t => t.EmpleadaId == empleadaId
                            && t.FechaHora.Date == dia.Date
                            && t.Estado != EstadoTurno.Cancelado
                            && t.Estado == EstadoTurno.Pendiente)
                .ToList();
        }

        public IEnumerable<Turno> ObtenerTurnosDelDia(DateTime dia)
        {
            return _context.Turnos
                           .Include(t => t.Detalles)
                               .ThenInclude(d => d.Extras)
                           .Include(t => t.Cliente)
                           .Where(t => t.FechaHora.Date == dia.Date
                                       && t.Estado != EstadoTurno.Cancelado
                                       && t.Estado == EstadoTurno.Pendiente)
                           .ToList();
        }

        public List<Turno> ObtenerParaFechaYEmpleado(DateTime fecha, int empleadaId)
        {
            return _context.Turnos
                .Include(t => t.Detalles)
                    .ThenInclude(d => d.Extras)
                .Where(t => t.EmpleadaId == empleadaId && t.FechaHora.Date == fecha.Date && t.Estado != EstadoTurno.Cancelado)
                .ToList();
        }




    }
}
