using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Domain.Entities
{
    public class Empleado
    {
        public long IdEmpleado { get; set; }
        public long IdPersona { get; set; }

        public string CodigoEmpleado { get; set; } = default!;
        public bool EstaActivo { get; set; } = true;

        public Profesion Profesion { get; set; } = default!;
        public int IdProfesion { get; set; }

        public Departamento Departamento { get; set; } = default!;  
        public int IdDepartamento {  get; set; }

        public Posiciones Posiciones { get; set; } = default!;
        public int IdPosicion { get; set; }

        public DatosPersona Persona { get; set; } = default!;

        protected Empleado() { } // EF

        public Empleado(DatosPersona persona, string codigoEmpleado, int cargo)
        {
            Persona = persona ?? throw new ArgumentNullException(nameof(persona));
            ActualizarDatosEmpleado(codigoEmpleado, cargo);
        }

        public void ActualizarDatosEmpleado(string codigoEmpleado, int cargo)
        {
            if (string.IsNullOrWhiteSpace(codigoEmpleado))
                throw new ArgumentException("El código de empleado es requerido.", nameof(codigoEmpleado));

            CodigoEmpleado = codigoEmpleado.Trim();
            IdPosicion = cargo;
        }

        public void CambiarEstatus(bool estaActivo) => EstaActivo = estaActivo;
    }

}
