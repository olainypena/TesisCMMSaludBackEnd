using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Medico
{
    public class MedicoLstDto
    {
        public long IdMedico { get; set; }

        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public string NombreCompleto => $"{Nombres} {Apellidos}";
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }

        public string NumeroColegiado { get; set; } = default!;        
        public bool EstaActivo { get; set; }

        // 👇 ESTOS SON LOS QUE FALTAN
        public string Especialidad { get; set; } = string.Empty;
        public string Horario { get; set; } = string.Empty;
    }
}
