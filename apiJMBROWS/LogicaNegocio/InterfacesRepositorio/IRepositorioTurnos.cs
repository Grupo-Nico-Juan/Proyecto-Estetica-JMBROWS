using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioTurnos : IRepositorio<Turno>
    {
        public IEnumerable<Turno> ObtenerTurnosDelDiaPorEmpleada(int empleadaId, DateTime dia);

        public IEnumerable<Turno> BuscarPorEmpleada(int empleadaId);

        public IEnumerable<Turno> ObtenerTurnosDelDia(DateTime dia);
        public List<Turno> ObtenerParaFechaYEmpleado(DateTime fecha, int empleadaId);

    }
}
