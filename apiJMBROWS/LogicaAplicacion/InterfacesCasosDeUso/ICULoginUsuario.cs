using Libreria.LogicaNegocio.Entidades;
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
        Administrador LoginUsuario(LoginDTO dto);
    }

}
