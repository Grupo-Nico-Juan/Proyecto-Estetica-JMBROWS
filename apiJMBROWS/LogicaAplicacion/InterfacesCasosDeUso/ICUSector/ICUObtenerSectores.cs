using System.Collections.Generic;
using LogicaAplicacion.Dtos.SectorDTO;

public interface ICUObtenerSectores
{
    IEnumerable<SectorDTO> Ejecutar();
}