namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class ActualizarDetalleTurnoDTO
    {
        public int Id { get; set; }
        public int TurnoId { get; set; }
        public int ServicioId { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
    }
}