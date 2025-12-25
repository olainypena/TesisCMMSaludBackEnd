using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Ocupacion
{
    public class OcupacionUpdDto
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public int IdOcupacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name ="ocupacion")]
        [StringLength(25)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool EstaActivo { get; set; } = true;
    }
}
