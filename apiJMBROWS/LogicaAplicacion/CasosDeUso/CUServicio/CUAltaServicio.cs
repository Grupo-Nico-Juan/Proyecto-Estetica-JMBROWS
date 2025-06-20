using LogicaAplicacion.Dtos.SectorDTO;
using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUAltaServicio : ICUAltaServicio
    {
        private readonly IRepositorioServicios _repoServicios;
        private readonly IRepositorioSectores _repoSectores;

        public CUAltaServicio(IRepositorioServicios repoServicios, IRepositorioSectores repoSectores)
        {
            _repoServicios = repoServicios;
            _repoSectores = repoSectores;
        }

        public void Ejecutar(AltaServicioDTO dto)
        {
            var sector = _repoSectores.GetById(dto.SectorId);
            if (sector == null)
                throw new Exception("Sector no encontrado.");

            if (sector.SucursalId != dto.SucursalId)
                throw new Exception("El sector no pertenece a la sucursal indicada.");

            var nuevo = new Servicio
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                DuracionMinutos = dto.DuracionMinutos,
                Precio = dto.Precio,
                Sectores = new List<Sector> { sector }
            };

            nuevo.EsValido();
            _repoServicios.Add(nuevo);
        }

    }
}





