namespace LogicaAplicacion.Dtos.SectorDTO
{
    public class ActualizarSectorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int SucursalId { get; set; }
        public string? Descripcion { get; set; }
    }
}