namespace LogicaAplicacion.Dtos.ServicioDTO
{
    public class AltaServicioDTO
    {
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        // Si luego quieres asociar habilidades al crear, puedes agregar:
        // public List<int> IdsHabilidades { get; set; }
    }
}