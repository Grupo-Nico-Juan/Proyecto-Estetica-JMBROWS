using LogicaAplicacion.Dtos.ClienteDTO;

public interface ICUObtenerClientePorTelefono
{
    ClienteDTO? Ejecutar(string telefono);
}