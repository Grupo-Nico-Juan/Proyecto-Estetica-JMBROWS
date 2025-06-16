using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

public class CUAltaPeriodoLaboral : ICUAltaPeriodoLaboral
{
    private readonly IRepositorioPeriodoLaboral _repo;

    public CUAltaPeriodoLaboral(IRepositorioPeriodoLaboral repo)
    {
        _repo = repo;
    }

    public void Ejecutar(AltaPeriodoLaboralDTO dto)
    {
        var periodo = new PeriodoLaboral
        {
            EmpleadaId = dto.EmpleadaId,
            Tipo = dto.Tipo,
            DiaSemana = dto.DiaSemana,
            HoraInicio = dto.HoraInicio,
            HoraFin = dto.HoraFin,
            Desde = dto.Desde,
            Hasta = dto.Hasta,
            Motivo = dto.Motivo
        };
        periodo.EsValido();
        _repo.Agregar(periodo);
    }
}
