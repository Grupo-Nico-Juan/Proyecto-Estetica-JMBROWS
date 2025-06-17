namespace LogicaAplicacion.Dtos.ClienteDTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EsRegistrado { get; set; }
    }
}