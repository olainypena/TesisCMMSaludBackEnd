using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.EstadoCivil
{
    public class EstadoCivilUpdDto
    {
        [Required(ErrorMessage ="El campo {0} es requrido")]
        public int IdEstadoCivil { get; private set; }
        [Required(ErrorMessage = "El campo {0} es requrido")]
        [Display(Name = "estado civil")]
        [StringLength(20)]
        public string Nombre { get; private set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requrido")]
        public bool EstaActivo { get; private set; } = true;
    }
}
