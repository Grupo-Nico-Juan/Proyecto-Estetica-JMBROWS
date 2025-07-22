namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class EmpleadoTurnoDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string? Color { get; set; }
        public required string Cargo { get; set; }
    }
}