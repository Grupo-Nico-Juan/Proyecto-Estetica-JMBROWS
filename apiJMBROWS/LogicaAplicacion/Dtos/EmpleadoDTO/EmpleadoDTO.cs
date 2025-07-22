namespace LogicaAplicacion.Dtos.EmpleadoDTO
{
    public class EmpleadoDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string? Color { get; set; }
        public required string Cargo { get; set; }
        public int? SucursalId { get; set; }
    }
}