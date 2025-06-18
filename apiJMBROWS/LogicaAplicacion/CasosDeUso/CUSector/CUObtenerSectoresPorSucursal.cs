using LogicaAplicacion.Dtos.SectorDTO;
using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSector;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;

public class CUObtenerSectoresPorSucursal : ICUObtenerSectoresPorSucursal
{
    private readonly IRepositorioSectores _repo;

public CUObtenerSectoresPorSucursal(IRepositorioSectores repo)
    {
        _repo = repo;
    }

    public IEnumerable<SectorDTOSuc> Ejecutar(int sucursalId)
    {
        var sectores = _repo.ObtenerSectoresConServiciosPorSucursal(sucursalId);
        if (sectores == null || !sectores.Any())
            throw new Exception("No se encontraron sectores para la sucursal.");

        return sectores.Select(s => new SectorDTOSuc
        {
            Id = s.Id,
            Nombre = s.Nombre,
            Descripcion = s.Descripcion,
            SucursalId = s.SucursalId,
            Servicios = s.Servicios.Select(serv => new ServicioDTOS
            {
                Id = serv.Id,
                Nombre = serv.Nombre,
                Precio = serv.Precio,
                DuracionMinutos = serv.DuracionMinutos,
                Descripcion = serv.Descripcion
            }).ToList()
        });
    }
}