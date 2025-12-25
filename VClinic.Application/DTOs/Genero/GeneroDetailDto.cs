using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Genero
{
    public class GeneroDetailDto
    {
        public int IdGenero { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EstaActivo { get; set; } = true;
    }
}
