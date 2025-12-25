using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class EstadoCivil
    {
        public EstadoCivil(string nombre, bool estaActivo)
        {
            Nombre = nombre;
            EstaActivo = estaActivo;
        }

        public int IdEstadoCivil { get;  set; }       
        public string Nombre { get;  set; } =  null!;
        public bool EstaActivo { get;  set; } = true;

        public ICollection<DatosPersona> Personas { get; set; } = null!;

        public void ActualizarEstadoCivil(int idEstadoCivil, string nombre, bool estaActivo)
        {
            IdEstadoCivil = idEstadoCivil;
            Nombre = nombre;
            EstaActivo = estaActivo;
        }

        public void UpdateStatus(bool estatusEstadoCivil)
        {
            EstaActivo = estatusEstadoCivil;
        }
    }
}
