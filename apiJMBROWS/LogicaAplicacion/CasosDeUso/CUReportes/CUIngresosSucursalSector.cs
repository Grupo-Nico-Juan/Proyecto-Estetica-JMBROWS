using LogicaAplicacion.Dtos.ReportesDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUReportes;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

using LogicaNegocio.Entidades.Enums;
namespace LogicaAplicacion.CasosDeUso.CUReportes
{
    public class CUIngresosSucursalSector : ICUIngresosSucursalSector
    {
        private readonly IRepositorioTurnos _repoTurnos;
        private readonly IRepositorioSucursales _repoSucursales;
        private readonly IRepositorioSectores _repoSectores;

        public CUIngresosSucursalSector(IRepositorioTurnos repoTurnos,
            IRepositorioSucursales repoSucursales,
            IRepositorioSectores repoSectores)
        {
            _repoTurnos = repoTurnos;
            _repoSucursales = repoSucursales;
            _repoSectores = repoSectores;
        }

        public IEnumerable<IngresosSucursalDTO> Ejecutar(int anio, int mes)
        {
            var sectores = _repoSectores.GetAll()
                .ToDictionary(s => s.Id, s => s.Nombre.ToLower());
            var turnos = _repoTurnos.GetAll()
                .Where(t => t.FechaHora.Year == anio && t.FechaHora.Month == mes &&
                            t.Estado == EstadoTurno.Realizado);
            var sucursales = _repoSucursales.GetAll();
            var lista = new List<IngresosSucursalDTO>();

            foreach (var suc in sucursales)
            {
                decimal cejas = 0, unas = 0, pestanas = 0, otros = 0;
                var turnosSucursal = turnos.Where(t => t.SucursalId == suc.Id);
                foreach (var t in turnosSucursal)
                {
                    string nombreSector = "otros";
                    if (t.SectorId.HasValue && sectores.TryGetValue(t.SectorId.Value, out var nom))
                        nombreSector = nom;

                    decimal total = t.PrecioTotal();
                    if (nombreSector.Contains("ceja")) cejas += total;
                    else if (nombreSector.Contains("uña")) unas += total;
                    else if (nombreSector.Contains("pesta")) pestanas += total;
                    else otros += total;
                }
                lista.Add(new IngresosSucursalDTO
                {
                    Sucursal = suc.Nombre,
                    Cejas = cejas,
                    Unas = unas,
                    Pestanas = pestanas,
                    Otros = otros
                });
            }
            return lista;
        }
    }
}
