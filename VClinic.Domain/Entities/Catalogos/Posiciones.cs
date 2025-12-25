using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class Posiciones
    {
        public Posiciones(string nombre, int nivel, bool estaActivo)
        {
            Nombre = nombre;            
            Nivel = nivel;
            EstaActivo = estaActivo;
            
        }

        public int IdPosicion { get; set; }
        public string Nombre { get; set; } = null!;
        public int Nivel { get; set; }
        public bool EstaActivo { get; set; } = true;

        public IEnumerable<Empleado> Empleados { get; set; } = null!;

        public void ActualizarPosicion(int idPosicion, string nombre, int nivel, bool estaActivo)
        {
            IdPosicion = idPosicion;
            Nombre = nombre;
            Nivel = nivel;
            EstaActivo = estaActivo;

        }

        public void UpdateStatus(bool estatusPosicion)
        {
            EstaActivo = estatusPosicion;
        }
    }
}
