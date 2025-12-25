using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class Ocupacion
    {
        public Ocupacion(string nombre, bool estaActivo)
        {
            Nombre = nombre;
            this.EstaActivo = estaActivo;
        }

        public int IdOcupacion { get; set; }        
        public string Nombre { get; set; } = null!;
        public bool EstaActivo { get; set; } = true;

        public ICollection<Paciente> Pacientes { get; set; } = null!;

        public void ActualizarOcupacion(int idOcupacion, string nombre, bool estaActivo)
        {
            IdOcupacion = idOcupacion;
            Nombre = nombre;
            this.EstaActivo = estaActivo;
        }

        public void UpdateStatus(bool estatusOcupacion)
        {
            EstaActivo = estatusOcupacion;
        }
    }
}
