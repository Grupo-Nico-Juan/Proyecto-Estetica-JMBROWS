
namespace LogicaAplicacion.Dtos.ExtraServicioDTO
{
    public class ExtraServicioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public int ServicioId { get; set; }
    }
}
