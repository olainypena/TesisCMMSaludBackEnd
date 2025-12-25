using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class TipoIdentificacion
    {
        public int IdTipoIdentificacion { get; set; }
        public string Codigo { get; set; } = null!;  // "CEDULA", "PASAPORTE"
        public string Nombre { get; set; } = null!;  // "Cédula de Identidad", etc.
        public bool EstaActivo { get; set; } = true;

        public ICollection<DatosPersona> Personas { get; set; } = null!;

        private TipoIdentificacion() { }

        public TipoIdentificacion(string codigo, string nombre)
        {
            ActualizarDatos(0, codigo, nombre, true);
        }

        public TipoIdentificacion(string codigo, string nombre, bool activo)
        {
            Nombre = nombre;
            Codigo = codigo;
            ActualizarEstatus(activo);
        }

        public void ActualizarEstatus(bool activo)
        {
            EstaActivo = activo;
        }

        public void ActualizarDatos(int Id, string codigo, string nombre, bool estaActivo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código es obligatorio.", nameof(codigo));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            IdTipoIdentificacion = Id;
            Codigo = codigo.Trim().ToUpperInvariant();
            Nombre = nombre.Trim();
            EstaActivo = estaActivo;
        }

        public void Activar() => EstaActivo = true;
        public void Desactivar() => EstaActivo = false;


    }
}
