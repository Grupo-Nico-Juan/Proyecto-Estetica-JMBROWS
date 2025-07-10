using LogicaAplicacion.Dtos.ReportesDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUReportes;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades.Enums;
using System.Collections.Generic;
using System.Linq;

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
            var sucursales = _repoSucursales.GetAll();
            var turnos = _repoTurnos.GetAll()
                .Where(t => t.FechaHora.Year == anio && t.FechaHora.Month == mes &&
                            t.Estado == EstadoTurno.Realizado);

            var lista = new List<IngresosSucursalDTO>();

            foreach (var suc in sucursales)
            {
                decimal cejas = 0, unas = 0, pestanas = 0, otros = 0;

                // Filtrar turnos de la sucursal
                var turnosSucursal = turnos.Where(t => t.SucursalId == suc.Id);

                foreach (var t in turnosSucursal)
                {
                    // Para cada detalle del turno, buscar el sector del servicio
                    foreach (var detalle in t.Detalles)
                    {
                        // Un servicio puede estar en varios sectores, pero tomamos el primero (o puedes iterar todos)
                        var sector = detalle.Servicio?.Sectores.FirstOrDefault(s => sectores.ContainsKey(s.Id));
                        string nombreSector = sector != null ? sectores[sector.Id] : "otros";

                        decimal total = (detalle.Servicio?.Precio ?? 0) + detalle.Extras.Sum(e => e.Precio);

                        if (nombreSector.Contains("ceja")) cejas += total;
                        else if (nombreSector.Contains("uña")) unas += total;
                        else if (nombreSector.Contains("pesta")) pestanas += total;
                        else otros += total;
                    }
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