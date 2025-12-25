using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.GrupoSanguinio
{
    public class GrupoSanguinioDetailDto
    {
        public int IdGrupoSangre { get; set; }       
        public string TipoSangre { get; set; } = string.Empty;
        public string GrupoABO { get; set; } = string.Empty;
        public string FactorRH { get; set; } = string.Empty;
        public bool EstaActivo { get; set; } = true;

    }
}
