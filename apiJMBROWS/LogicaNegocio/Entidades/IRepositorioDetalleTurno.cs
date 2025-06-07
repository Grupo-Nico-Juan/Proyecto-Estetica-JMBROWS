using LogicaNegocio.Entidades;

public interface IRepositorioDetalleTurno
{
    void Add(DetalleTurno detalle);
    void Update(int id, DetalleTurno detalle);
    void Remove(int id);
    DetalleTurno GetById(int id);
    IEnumerable<DetalleTurno> GetAll();
}