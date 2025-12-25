using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.EstadoCivil
{
    public class EstadoCivilLstDto
    {
        public int IdEstadoCivil { get;  set; }
        public string Nombre { get;  set; } = null!; 
        public bool EstaActivo { get;  set; } = true;
    }
}
