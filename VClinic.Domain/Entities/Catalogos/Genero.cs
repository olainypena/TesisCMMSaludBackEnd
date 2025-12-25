using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class Genero
    {

        public Genero(string nombre, string codigo, bool estaActivo)
        {
            Nombre = nombre;
            Codigo = codigo;
            EstaActivo = estaActivo;
        }

        public int IdGenero { get;  set; }
        public string Codigo { get;  set; } = null!;  
        public string Nombre { get;  set; } = null!;  
        public bool EstaActivo { get;  set; } = true;

        public ICollection<DatosPersona> Personas { get; set; } = null!;

        public void ActualizarGenero(int idGenero, string nombre, string codigo, bool estaActivo)
        {
            IdGenero = idGenero;
            Nombre = nombre;    
            Codigo = codigo;
            EstaActivo = estaActivo;
        }

        public void UpdateStatus(bool estatusGenero)
        {
           EstaActivo = estatusGenero;
        }
    }
}
