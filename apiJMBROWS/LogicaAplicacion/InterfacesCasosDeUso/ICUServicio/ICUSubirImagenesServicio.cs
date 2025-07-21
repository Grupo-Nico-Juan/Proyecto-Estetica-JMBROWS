using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUServicio
{
    public interface ICUSubirImagenesServicio
    {
        Task SubirAsync(int servicioId, List<IFormFile> archivos);
    }
}
