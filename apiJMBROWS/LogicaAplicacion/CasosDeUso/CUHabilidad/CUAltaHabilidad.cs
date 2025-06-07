using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUHabilidad
{
    public class CUAltaHabilidad : ICUAltaHabilidad
    {
        private readonly IRepositorioHabilidades _repo;

        public CUAltaHabilidad(IRepositorioHabilidades repo)
        {
            _repo = repo;
        }

        public void Ejecutar(AltaHabilidadDTO dto)
        {
            var nueva = new Habilidad
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };
            // Si tienes validaciones, agrégalas aquí
            _repo.Add(nueva);
        }
    }
}