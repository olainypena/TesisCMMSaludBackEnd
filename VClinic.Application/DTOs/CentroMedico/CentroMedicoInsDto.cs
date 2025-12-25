using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.CentroMedico
{
    public sealed class CentroMedicoInsDto
    {
        [Display(Name = "Nombre del centro médico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "descripción del centro médico")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Descripcion { get; set; } = null!;

        [Display(Name = "dirección del centro médico")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        [StringLength(150, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Direccion { get; set; } = null!;

        [Display(Name = "telefono del centro médico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Phone(ErrorMessage = "El campo {0} no es valido.")]
        [StringLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Telefono { get; set; } = null!;

        [Display(Name = "estatus del centro médico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public bool EstaActivo { get; set; } = true;

        [Display(Name = "fecha fundación del centro médico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public DateTime FechaFundacion { get; set; }

        [Display(Name = "RNC del centro médico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Rnc { get; set; } = null!;

        [Display(Name = "tipo de centro médico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int Tipo { get; set; }

        [Display(Name = "código del centro médico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(5, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Codigo { get; set; } = null!;
    }
}
