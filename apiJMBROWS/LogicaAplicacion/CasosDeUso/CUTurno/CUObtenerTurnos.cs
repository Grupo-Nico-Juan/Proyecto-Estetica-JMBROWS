using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades.Enums;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUObtenerTurnos : ICUObtenerTurnos
    {
        private readonly IRepositorioTurnos _repo;

        public CUObtenerTurnos(IRepositorioTurnos repo)
        {
            _repo = repo;
        }

        public IEnumerable<TurnoDTO> Ejecutar()
        {
            return _repo.GetAll().Select(t => new TurnoDTO
            {
                Id = t.Id,
                FechaHora = t.FechaHora,
                EmpleadaId = t.EmpleadaId,
                ClienteId = t.ClienteId,
                SucursalId = t.SucursalId,
                Estado = t.Estado,
                Detalles = t.Detalles.Select(d => new DetalleTurnoDTO
                {
                    ServicioId = d.ServicioId,

                }).ToList()
            });
        }
    }
}