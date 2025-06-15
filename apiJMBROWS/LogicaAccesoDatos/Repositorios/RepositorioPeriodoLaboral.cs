using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioPeriodoLaboral : IRepositorioPeriodoLaboral
    {
        private readonly EsteticaContext _context;
        public RepositorioPeriodoLaboral(EsteticaContext context)
        {
            _context = context;
        }
        public void Agregar(PeriodoLaboral periodo)
        {
            _context.PeriodosLaborales.Add(periodo);
            _context.SaveChanges();
        }

        public IEnumerable<PeriodoLaboral> ObtenerPorEmpleada(int empleadaId)
        {
            return _context.PeriodosLaborales
                .Where(p => p.EmpleadaId == empleadaId)
                .ToList();
        }

        public PeriodoLaboral? ObtenerPorId(int id)
        {
            return _context.PeriodosLaborales.Find(id);
        }

        public void Eliminar(int id)
        {
            var periodo = _context.PeriodosLaborales.Find(id);
            if (periodo != null)
            {
                _context.PeriodosLaborales.Remove(periodo);
                _context.SaveChanges();
            }
        }

        public void Modificar(PeriodoLaboral periodo)
        {
            _context.PeriodosLaborales.Update(periodo);
            _context.SaveChanges();
        }

        public IEnumerable<PeriodoLaboral> ObtenerTodos()
        {
            return _context.PeriodosLaborales.ToList();
        }
    }
}
