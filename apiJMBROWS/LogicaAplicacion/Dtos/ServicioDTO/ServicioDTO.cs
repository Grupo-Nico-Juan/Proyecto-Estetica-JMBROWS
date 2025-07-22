
using LogicaAplicacion.Dtos.ExtraServicioDTO;

namespace LogicaAplicacion.Dtos.ServicioDTO
{
    public class ServicioDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public List<ServiciosExtrasDTO> Extras { get; set; } = new();
        public List<ServicioImagenDTO> Imagenes { get; set; } = new();

    }
}