using LogicaAplicacion.Dtos.SectorDTO;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;

public class CUActualizarSector : ICUActualizarSector
{
    private readonly IRepositorioSectores _repo;
    public CUActualizarSector(IRepositorioSectores repo) { _repo = repo; }

    public void Ejecutar(ActualizarSectorDTO dto)
    {
        var sector = _repo.GetById(dto.Id);
        if (sector == null)
            throw new Exception("Sector no encontrado");

        sector.Nombre = dto.Nombre;
        _repo.Update(dto.Id, sector);
    }
}