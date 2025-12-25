using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.GrupoSanguinio
{
    public class GrupoSanguinioUpdDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int IdGrupoSangre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Tipo de Sangre")]
        [StringLength(3)]
        public string TipoSangre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Grupo ABO")]
        [StringLength(2)]
        public string GrupoABO { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Factor RH")]
        [StringLength(10)]
        public string FactorRH { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool EstaActivo { get; private set; } = true;

    }
}
