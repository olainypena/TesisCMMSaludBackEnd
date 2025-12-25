using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.GrupoSanguinio
{
    public class GrupoSanguinioLstDto
    {        
        public int IdGrupoSangre { get; set; }
        public string TipoSangre { get; set; } = string.Empty;       
        public bool EstaActivo { get; private set; } = true;
    }
}
