﻿
using LogicaAplicacion.Dtos.DtoUsuario;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios
{
    public interface ICUAltaUsuario
    {
        void AltaUsuario(RegistroAdministradorDTO dto);
        void AltaUsuario(RegistroEmpleadoDTO dto);

    }
}
