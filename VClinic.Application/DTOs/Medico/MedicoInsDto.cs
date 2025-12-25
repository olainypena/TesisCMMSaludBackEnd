using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.DTOs.Persona;

namespace VClinic.Application.DTOs.Medico
{
    public class MedicoInsDto: PersonaInsDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "número de colegiado")]
        [StringLength(50)]
        public string NumeroColegiado { get; set; } = default!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool EstaActivo { get; set; } = true;

        public string Especialidad { get; set; } = default!;
        public string Horario { get; set; } = default!;

    }
}
