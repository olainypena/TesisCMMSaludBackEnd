using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Paciente
{
    public class PacienteLstDto
    {
        public long IdPaciente { get; set; }    

        // Persona
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public string NombreCompleto => $"{Nombres} {Apellidos}";
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }

        // Paciente
        public int IdGrupoSangre { get; set; }
        public int IdHistorial { get; set; } = default!;
        public bool EstaActivo { get; set; }
    }
}
