namespace LogicaAplicacion.Dtos.EmpleadoDTO
{
    public class AltaEmpleadoDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string PasswordPlano { get; set; }
        public string Cargo { get; set; }
        public int SucursalId { get; set; }
        // Puedes agregar más campos según la entidad Empleado
    }
}