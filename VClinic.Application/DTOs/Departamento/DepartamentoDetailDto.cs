using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Departamento
{
    public class DepartamentoDetailDto
    {
        public int IdDepartamento { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EstaActivo { get; set; } = true;
    }
}
