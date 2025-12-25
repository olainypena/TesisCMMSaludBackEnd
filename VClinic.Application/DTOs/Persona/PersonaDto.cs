using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Persona
{
    public class PersonaDto
    {
        public long IdPersona { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "nombres")]
        [StringLength(100)]
        public string Nombres { get; set; } = default!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "apellidos")]
        [StringLength(100)]
        public string Apellidos { get; set; } = default!;

        [Display(Name = "fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "teléfono")]
        [StringLength(20)]
        public string? Telefono { get; set; }

        [Display(Name = "Celular")]
        [StringLength(20)]
        public string? Celular { get; set; }

        [EmailAddress(ErrorMessage = "El campo {0} no tiene un formato válido.")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Display(Name = "dirección")]
        [StringLength(250)]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "tipo de identificación")]
        public int IdTipoIdentificacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "identificación")]
        [StringLength(20)]
        public string NumeroIdentificacion { get; set; } = default!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "estado civil")]
        public int IdEstadoCivil { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "ocupacion")]
        public int IdOcupacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "genero")]
        public int IdGenero { get; set; }

    }
}
