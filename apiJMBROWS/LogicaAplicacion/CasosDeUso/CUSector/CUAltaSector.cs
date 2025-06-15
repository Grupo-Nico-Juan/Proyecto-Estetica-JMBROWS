using LogicaAplicacion.Dtos.SectorDTO;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

public class CUAltaSector : ICUAltaSector
{
    private readonly IRepositorioSectores _repo;
    public CUAltaSector(IRepositorioSectores repo) { _repo = repo; }
    public void Ejecutar(AltaSectorDTO dto)
    {
        var sector = new Sector
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            SucursalId = dto.SucursalId
        };
        sector.EsValido();
        _repo.Add(sector);
    }
}