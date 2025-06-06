using LogicaAplicacion.Dtos.SucursalDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUSucursal
{
    public class CUAltaSucursal : ICUAltaSucursal
    {
        private readonly IRepositorioSucursales _repo;

        public CUAltaSucursal(IRepositorioSucursales repo)
        {
            _repo = repo;
        }

        public void Ejecutar(AltaSucursalDTO dto)
        {
            var nueva = new Sucursal
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono
            };
            nueva.EsValido();
            _repo.Add(nueva);
        }
    }

}
