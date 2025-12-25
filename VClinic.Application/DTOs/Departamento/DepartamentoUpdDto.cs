using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Departamento
{
    public class DepartamentoUpdDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int IdDepartamento { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "departamento")]
        [StringLength(10)]
        public string Codigo { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "departamento")]
        [StringLength(30)]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool EstaActivo { get; set; } = true;
    }
}
