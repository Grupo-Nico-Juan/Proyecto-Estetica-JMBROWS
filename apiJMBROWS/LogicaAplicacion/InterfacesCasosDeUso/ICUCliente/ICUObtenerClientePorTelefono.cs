using LogicaAplicacion.Dtos.ClienteDTO;


namespace LogicaAplicacion.InterfacesCasosDeUso.ICUCliente
{
    public interface ICUObtenerClientePorTelefono
    {
        ClienteDTO? Ejecutar(string telefono);
    }
}
