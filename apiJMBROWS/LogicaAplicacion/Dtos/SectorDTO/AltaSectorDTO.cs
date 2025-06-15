namespace LogicaAplicacion.Dtos.SectorDTO
{
    public class AltaSectorDTO
    {
        public required string Nombre { get; set; }
        public required int SucursalId { get; set; }
        public string? Descripcion { get; set; }
    }
}