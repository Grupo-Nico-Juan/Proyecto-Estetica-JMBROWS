
using LogicaAplicacion.Dtos.ExtraServicioDTO;

namespace LogicaAplicacion.Dtos.ServicioDTO
{
    public class ServicioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public List<ServiciosExtrasDTO> Extras { get; set; } = new();

    }
}