namespace LogicaAplicacion.Dtos.HabilidadDTO
{
    public class ActualizarHabilidadDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
    }
}