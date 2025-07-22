namespace LogicaAplicacion.Dtos.EmpleadoDTO
{
    public class AltaEmpleadoDTO
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Color { get; set; }
        public required string Cargo { get; set; }
        public int SucursalId { get; set; }
        // Puedes agregar más campos según la entidad Empleado
    }
}