using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Genero
{
    public class GeneroUpdDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int IdGenero {  get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "genero")]
        [StringLength(10)]
        public string Codigo { get; private set; } = null!;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "genero")]
        [StringLength(20)]
        public string Nombre { get; private set; } = null!;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool EstaActivo { get; private set; } = true;
    }
}
