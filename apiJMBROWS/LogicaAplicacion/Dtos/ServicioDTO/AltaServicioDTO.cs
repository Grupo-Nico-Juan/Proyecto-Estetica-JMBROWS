namespace LogicaAplicacion.Dtos.ServicioDTO
{
    public class AltaServicioDTO
    {
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        // Si luego quieres asociar habilidades al crear, puedes agregar:
        //public List<int> IdsHabilidades { get; set; } = new();
        //esto se pide solo para la validacion de la sucursal y sector
        public required int SucursalId { get; set; }
        public required int SectorId { get; set; }
    }
}