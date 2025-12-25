using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities
{
    public sealed class CentroMedico
    {
        public int IdCentroMedico { get; private set; }

        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public bool EstaActivo { get; set; } = true;
        public DateTime FechaFundacion { get; set; }
        public string Rnc { get; set; } = null!;
        public int Tipo { get; set; }
        public string Codigo { get; set; } = null!;

        public CentroMedico() { }

        public CentroMedico(
            int idCentroMedico,
            string nombre,
            string descripcion,
            string direccion,
            string telefono,
            bool esActivo,
            DateTime fechaFundacion,
            string rnc,
            int tipo,
            string codigo)
        {
            ActualizarCentroMedico(idCentroMedico,nombre, descripcion, direccion, telefono,esActivo, fechaFundacion, rnc, tipo, codigo);
        }

        public void ActualizarCentroMedico(int id,string nombre, string descripcion, string direccion, string telefono,bool esActivo, DateTime fechaFundacion, string rnc, int tipo, string codigo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("El campo descripcion es obligatorio.", nameof(descripcion));
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentException("La dirección es obligatoria.", nameof(direccion));
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El telefono es obligatorio.", nameof(telefono));
            if (string.IsNullOrWhiteSpace(rnc))
                throw new ArgumentException("El rnc es obligatorio.", nameof(rnc));
           
            IdCentroMedico = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Direccion = direccion;
            Telefono = telefono;
            EstaActivo = esActivo;
            FechaFundacion = fechaFundacion;
            Rnc = rnc;
            Tipo = tipo;
            Codigo = codigo;
        }

        public void UpdateStatus(bool ActivarCentro)
        {
            EstaActivo = ActivarCentro;;
        }
    }
}
