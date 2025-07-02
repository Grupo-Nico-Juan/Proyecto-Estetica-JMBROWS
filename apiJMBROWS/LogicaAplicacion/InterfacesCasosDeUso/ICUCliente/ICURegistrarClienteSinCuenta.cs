using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUCliente
{
    public interface ICURegistrarClienteSinCuenta
    {
        ReservaClienteDTO Ejecutar(ReservaClienteDTO dto);
    }
}