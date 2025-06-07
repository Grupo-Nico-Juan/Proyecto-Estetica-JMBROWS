namespace LogicaAplicacion.Dtos.TurnoDTO
{
    public class AltaDetalleTurnoDTO
    {
        public int TurnoId { get; set; }
        public int ServicioId { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
    }
}