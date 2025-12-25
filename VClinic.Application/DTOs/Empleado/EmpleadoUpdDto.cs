using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.DTOs.Persona;

namespace VClinic.Application.DTOs.Empleado
{
    public class EmpleadoUpdDto: PersonaDto
    {
        [Required]
        public long IdEmpleado { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "número de empleado")]
        [StringLength(10)]
        public string NumeroEmpleado { get; set; } = default!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int IdCargo { get; private set; } = default!;
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool EstaActivo { get; set; } = true;

    }
}
