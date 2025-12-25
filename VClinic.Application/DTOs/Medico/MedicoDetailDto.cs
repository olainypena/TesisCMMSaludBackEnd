using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Medico
{
    public class MedicoDetailDto
    {
        public long IdMedico { get; set; }

        // Persona
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; } = default!;

        // Medico
        public string NumeroColegiado { get; set; } = default!;        
        public bool EstaActivo { get; set; }

        public string Especialidad { get; set; } = default!;
        public string Horario { get; set; } = default!;

    }
}
