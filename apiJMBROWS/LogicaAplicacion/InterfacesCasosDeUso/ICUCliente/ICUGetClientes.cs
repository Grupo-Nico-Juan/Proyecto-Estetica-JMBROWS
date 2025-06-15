using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.EmpleadoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUCliente
{
    public interface ICUGetClientes
    {
        IEnumerable<ClienteDTO> Ejecutar();
    }
}
