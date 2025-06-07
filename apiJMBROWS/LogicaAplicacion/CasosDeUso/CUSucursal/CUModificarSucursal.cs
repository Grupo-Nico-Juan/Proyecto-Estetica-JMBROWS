using LogicaAplicacion.Dtos.SucursalDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUSurcursal;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUSucursal
{
    public class CUModificarSucursal : ICUModificarSucursal
    {
        private readonly IRepositorioSucursales _repo;

        public CUModificarSucursal(IRepositorioSucursales repo)
        {
            _repo = repo;
        }

        public void Ejecutar(SucursalDTO dto)
        {
            var sucursal = _repo.GetById(dto.Id);
            if (sucursal == null)
                throw new Exception("Sucursal no encontrada");

            sucursal.Nombre = dto.Nombre;
            sucursal.Direccion = dto.Direccion;
            sucursal.Telefono = dto.Telefono;
            sucursal.EsValido();

            _repo.Update(sucursal.Id,sucursal);
        }
    }
}