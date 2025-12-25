using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities.Catalogos;
using VClinic.Domain.Enums;

namespace VClinic.Domain.Entities
{
    public class DatosPersona
    {
        public long IdPersona { get; set; }

        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }

        public int IdTipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; } = null!;
        public int IdEstadoCivil { get; set; }
        public int IdGenero { get; set; }

        public TipoIdentificacion? TipoIdentificacion { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public Genero? Genero { get; set; }

        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }


        // Relaciones 1:1 (rol)
        public Paciente? Paciente { get; set; }
        public Medico? Medico { get; set; }
        public Empleado? Empleado { get; private set; }

        protected DatosPersona() { } // EF

        public DatosPersona(           
            string nombres,
            string apellidos,
            int tipoIdentificacion,
            string identificacion,
            int idEstadoCivil,
            int idGenero,
            DateTime? fechaNacimiento,
            string? telefono,
            string? celular,
            string? email,
            string? direccion)
        {            
            IdEstadoCivil = idEstadoCivil;
            IdGenero = idGenero;
            ActualizarDatosBasicos(nombres, apellidos, fechaNacimiento);
            ActualizarIdentificacion(tipoIdentificacion, identificacion);
            ActualizarContacto(telefono, celular, email, direccion);
        }

        public DatosPersona(
            long idPersona,
            string nombres,
            string apellidos,
            int tipoIdentificacion,
            string identificacion,
            int idEstadoCivil,
            int idGenero,
            int idOcupacion,
            DateTime? fechaNacimiento,
            string? telefono,
            string? celular,
            string? email,
            string? direccion)
        {
            IdPersona = idPersona;
            IdEstadoCivil = idEstadoCivil;
            IdGenero = idGenero;
            ActualizarDatosBasicos(nombres, apellidos, fechaNacimiento);
            ActualizarIdentificacion(tipoIdentificacion, identificacion);
            ActualizarContacto(telefono, celular, email, direccion);
        }

        public void ActualizarDatosBasicos(string nombres, string apellidos, DateTime? fechaNacimiento)
        {
            if (string.IsNullOrWhiteSpace(nombres))
                throw new ArgumentException("Los nombres son requeridos.", nameof(nombres));

            if (string.IsNullOrWhiteSpace(apellidos))
                throw new ArgumentException("Los apellidos son requeridos.", nameof(apellidos));

            Nombres = nombres.Trim();
            Apellidos = apellidos.Trim();
            FechaNacimiento = fechaNacimiento;
        }

        public void ActualizarIdentificacion(int tipoIdentificacion, string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
                throw new ArgumentException("La identificación es requerida.", nameof(identificacion));

            IdTipoIdentificacion = tipoIdentificacion;
            NumeroIdentificacion = identificacion.Trim();
        }

        public void ActualizarContacto(
            string? telefono,
            string? celular,
            string? email,
            string? direccion)
        {
            Telefono = telefono?.Trim();
            Celular = celular?.Trim();
            Email = email?.Trim();
            Direccion = direccion?.Trim();
        }
    }
}
