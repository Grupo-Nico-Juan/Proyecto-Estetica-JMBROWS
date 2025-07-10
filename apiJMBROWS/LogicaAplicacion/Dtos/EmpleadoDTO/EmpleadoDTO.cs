namespace LogicaAplicacion.Dtos.EmpleadoDTO
{
    public class EmpleadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Color { get; set; }
        public string Cargo { get; set; }
        public int? SucursalId { get; set; }
    }
}