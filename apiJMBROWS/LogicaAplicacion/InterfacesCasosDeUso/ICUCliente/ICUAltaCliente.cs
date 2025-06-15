using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUCliente
{
    public interface ICUAltaCliente
    {
        void AltaCliente(RegistroClienteDTO dto);
    }
}
