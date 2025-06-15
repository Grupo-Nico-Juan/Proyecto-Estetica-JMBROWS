using LogicaNegocio.InterfacesRepositorio;

public class CUEliminarSector : ICUEliminarSector
{
    private readonly IRepositorioSectores _repo;
    public CUEliminarSector(IRepositorioSectores repo) { _repo = repo; }

    public void Ejecutar(int id)
    {
        var sector = _repo.GetById(id);
        if (sector == null)
            throw new Exception("Sector no encontrado");
        _repo.Remove(id);
    }
}