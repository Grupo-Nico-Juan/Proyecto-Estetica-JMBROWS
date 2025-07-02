using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Enums;
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
        // Validación estricta según el tipo de periodo
        if (dto.Tipo == TipoPeriodoLaboral.HorarioHabitual)
        {
            if (dto.DiaSemana == null || dto.HoraInicio == null || dto.HoraFin == null)
                throw new Exception("Para un horario habitual debe especificar día de semana y horas.");
            if (dto.Desde != null || dto.Hasta != null || !string.IsNullOrEmpty(dto.Motivo))
                throw new Exception("No debe especificar fechas ni motivo para un horario habitual.");
        }
        else if (dto.Tipo == TipoPeriodoLaboral.Licencia)
        {
            if (dto.Desde == null || dto.Hasta == null)
                throw new Exception("Para una licencia debe especificar fechas.");
            if (dto.DiaSemana != null || dto.HoraInicio != null || dto.HoraFin != null)
                throw new Exception("No debe especificar día de semana ni horas para una licencia.");
        }
        else
        {
            throw new Exception("Tipo de periodo laboral no soportado.");
        }

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
