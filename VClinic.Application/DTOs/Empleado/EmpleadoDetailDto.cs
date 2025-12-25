using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Empleado
{
    public class EmpleadoDetailDto
    {
        public long IdEmpleado { get; set; }

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
        

        // Empleado
        public string NumeroEmpleado { get; set; } = default!;
        public int IdCargo { get; set; }
        public bool EstaActivo { get; set; }
    }
}
