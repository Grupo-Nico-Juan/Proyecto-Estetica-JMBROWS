using LogicaAplicacion.Dtos.DtoUsuario;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso
{
    public interface ICULoginUsuario
    {
        Usuario LoginUsuario(LoginDTO dto);
    }

}
