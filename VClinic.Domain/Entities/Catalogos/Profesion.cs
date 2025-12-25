using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class Profesion
    {
        public Profesion(string nombre, bool estaActivo)
        {
            Nombre = nombre;
            ActualizarEstatus(estaActivo);
        }

        public void ActualizarEstatus(bool estaActivo)
        {
            EstaActivo = estaActivo;
        }

        public void ActualizarProfesion(int idProfesion, string nombre, bool estaActivo)
        {
            IdProfesion = idProfesion;
            Nombre = nombre;
            ActualizarEstatus(estaActivo);
        }

        public int IdProfesion { get; set; }        
        public string Nombre { get; set; } = null!;
        public bool EstaActivo { get; set; } = true;

        public ICollection<Empleado> Empleados { get; set; } = null!;
    }
}
