namespace LogicaAplicacion.Dtos.ServicioDTO
{
    public class ActualizarServicioDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
    }
}