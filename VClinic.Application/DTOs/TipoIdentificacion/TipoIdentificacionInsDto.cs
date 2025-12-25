using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.TipoIdentificacion
{
    public class TipoIdentificacionInsDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "identificacion")]
        [StringLength(3)]
        public string Codigo { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "identificacion")]
        [StringLength(20)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool EstaActivo { get; set; } = true;
    }
}
