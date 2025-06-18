using LogicaAplicacion.Dtos.SectorDTO;
using LogicaNegocio.InterfacesRepositorio;

public class CUObtenerSectorPorId : ICUObtenerSectorPorId
{
    private readonly IRepositorioSectores _repo;
    public CUObtenerSectorPorId(IRepositorioSectores repo) { _repo = repo; }

    public SectorDTSSuc Ejecutar(int id)
    {
        var sector = _repo.GetById(id);
        if (sector == null)
            throw new Exception("Sector no encontrado");
        return new SectorDTSSuc
        {
            Id = sector.Id,
            Nombre = sector.Nombre,
            Descripcion = sector.Descripcion,
            SucursalId = sector.SucursalId
        };
    }
}