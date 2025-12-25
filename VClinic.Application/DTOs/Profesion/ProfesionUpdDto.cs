using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Profesion
{
    public class ProfesionUpdDto
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public int IdProfesion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name ="profesion")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool EstaActivo { get; set; } = true;
    }
}
