using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.TipoEquipamento
{
    public class TipoEquipamentoUpdDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdTipoEquipamento { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name ="tipo equipo")]
        [StringLength(20)]
        public string Nombre { get; set; } = default!;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "tipo equipo")]
        [StringLength(50)]
        public string Descripcion { get; set; } = default!;

        [Required(ErrorMessage ="El campo {0} es requerido")]
        public bool EstaActivo { get; set; } = true;
    }
}
