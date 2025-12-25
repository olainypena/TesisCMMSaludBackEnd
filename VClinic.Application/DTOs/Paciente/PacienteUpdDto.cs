using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.DTOs.Persona;

namespace VClinic.Application.DTOs.Paciente
{
    public class PacienteUpdDto : PersonaDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public long IdPaciente { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Tipo de Sangre")]
        public int IdGrupoSangre { get; set; }
        public bool EstaActivo { get; private set; } = true;
    }
}
