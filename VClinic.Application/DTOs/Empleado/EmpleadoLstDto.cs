using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Empleado
{
    public class EmpleadoLstDto
    {
        public long IdEmpleado { get; set; }

        // Persona
        public string Nombres { get; set; } = default!;
        public string Apellidos { get; set; } = default!;
        public string NombreCompleto => $"{Nombres} {Apellidos}";
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }

        // Empleado
        public string NumeroEmpleado { get; set; } = default!;
        public int IdCargo { get; set; } = default!;
        public bool EstaActivo { get; set; }
    }
}
