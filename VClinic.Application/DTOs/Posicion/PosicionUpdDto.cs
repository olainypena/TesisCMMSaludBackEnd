using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Posicion
{
    public class PosicionUpdDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdPosicion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "posicion")]
        [StringLength(30)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Nivel { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool EstaActivo { get; set; } = true;
    }
}
