using LogicaAplicacion.Dtos.ClienteDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUCliente
{
    public interface ICUObtenerClientePorId
    {
        ClienteDTO? Ejecutar(int clienteId);
    }
}
