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
        public IEnumerable<Turno> ObtenerTurnosDelDiaPorEmpleada(int empleadaId, DateTimeOffset dia);

        public IEnumerable<Turno> BuscarPorEmpleada(int empleadaId);

        public IEnumerable<Turno> ObtenerTurnosDelDia(DateTimeOffset dia);
        public List<Turno> ObtenerParaFechaYEmpleado(DateTimeOffset fecha, int empleadaId);

    }
}
