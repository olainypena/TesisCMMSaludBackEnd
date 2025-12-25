using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Equipamento
{
    public class EquipamentoInsDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "equipo")]
        [StringLength(35)]
        public string Nombre { get; set; } = default!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50)]
        public string Descripcion { get; set; } = default!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdTipoEquipo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool EstaActivo { get; set; } = true;
    }
}
