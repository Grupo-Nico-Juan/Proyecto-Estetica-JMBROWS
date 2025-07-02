using LogicaAplicacion.Dtos.SectorDTO;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

public class CUObtenerSectores : ICUObtenerSectores
{
    private readonly IRepositorioSectores _repo;
    public CUObtenerSectores(IRepositorioSectores repo) { _repo = repo; }

    public IEnumerable<SectorDTSSuc> Ejecutar()
    {
        return _repo.GetAll().Select(s => new SectorDTSSuc
        {
            Id = s.Id,
            Nombre = s.Nombre,
            Descripcion = s.Descripcion,
            SucursalId = s.SucursalId
        });
    }
}