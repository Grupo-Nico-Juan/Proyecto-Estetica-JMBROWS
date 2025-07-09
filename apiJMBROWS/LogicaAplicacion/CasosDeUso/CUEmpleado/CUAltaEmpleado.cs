using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUAltaEmpleado : ICUAltaEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUAltaEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(AltaEmpleadoDTO dto)
        {
            var nuevo = new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Color = dto.Color,
                Cargo = dto.Cargo,
                SucursalId = dto.SucursalId
            };
            nuevo.EsValidoEmpleado();
            _repo.AddEmpleado(nuevo);
        }

        private string HashPassword(string plainPassword)
        {
            // Implement password hashing logic here
            // For example, use a hashing library or custom logic
            return plainPassword; // Replace with actual hashed password
        }
    }
}