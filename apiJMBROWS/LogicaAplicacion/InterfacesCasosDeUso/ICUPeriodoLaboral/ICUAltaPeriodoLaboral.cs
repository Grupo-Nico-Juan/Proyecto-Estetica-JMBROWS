﻿using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral
{
    public interface ICUAltaPeriodoLaboral
    {
        void Ejecutar(AltaPeriodoLaboralDTO dto);
    }
}
