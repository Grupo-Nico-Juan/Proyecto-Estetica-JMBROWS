using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.EF;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioClientes : IRepositorioClientes
    {
        private readonly EsteticaContext _context;

        public RepositorioClientes(EsteticaContext context)
        {
            _context = context;
        }

        public void Add(Cliente obj)
        {
            obj.EsValido();
            _context.Clientes.Add(obj);
            _context.SaveChanges();
        }

        public IEnumerable<Cliente> GetAll()
        {
            return _context.Clientes.ToList();
        }

        public Cliente GetById(int id)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == id);
        }

        public void Remove(int id)
        {
            var c = GetById(id);
            if (c == null)
                throw new ClienteException($"No se encontró el cliente con ID {id}");

            _context.Clientes.Remove(c);
            _context.SaveChanges();
        }

        public void Remove(Cliente obj)
        {
            _context.Clientes.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(int id, Cliente obj)
        {
            var original = GetById(id);
            if (original == null)
                throw new ClienteException($"No se encontró el cliente con ID {id}");

            _context.Entry(original).CurrentValues.SetValues(obj);
            _context.SaveChanges();
        }
        public Cliente? GetByTelefono(string telefono)
        {
            return _context.Clientes.FirstOrDefault(c => c.Telefono == telefono);
        }

        public bool ExisteTelefono(string telefono)
        {
            return _context.Clientes.Any(c => c.Telefono == telefono);
        }

        public Cliente GetByEmail(string email)
        {
            return _context.Clientes.FirstOrDefault(c => c.Email == email);
        }

        public bool ExisteCorreoElectronico(string email)
        {
            return _context.Clientes.Any(c => c.Email == email);
        }
    }
}

