namespace Libreria.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        T GetById(int id);
        void Add(T obj);
        void Update(int id, T obj);
        void Remove(int id);
        void Remove(T obj);
        IEnumerable<T> GetAll();
    }
}
