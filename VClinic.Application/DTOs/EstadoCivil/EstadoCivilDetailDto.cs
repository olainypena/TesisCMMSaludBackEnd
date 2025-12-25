using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.EstadoCivil
{
    public class EstadoCivilDetailDto
    {
        public int IdEstadoCivil { get; private set; }
        public string Nombre { get; private set; } = null!;
        public bool EstaActivo { get; private set; } = true;
    }
}
