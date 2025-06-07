using LogicaAplicacion.Dtos.SectorDTO;
using LogicaNegocio.InterfacesRepositorio;

public class CUObtenerSectorPorId : ICUObtenerSectorPorId
{
    private readonly IRepositorioSectores _repo;
    public CUObtenerSectorPorId(IRepositorioSectores repo) { _repo = repo; }

    public SectorDTO Ejecutar(int id)
    {
        var sector = _repo.GetById(id);
        if (sector == null)
            throw new Exception("Sector no encontrado");
        return new SectorDTO
        {
            Id = sector.Id,
            Nombre = sector.Nombre,
        };
    }
}