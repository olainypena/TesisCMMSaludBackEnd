using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.TipoEquipamento
{
    public class TipoEquipamentoDetailDto
    {
        public int IdTipoEquipamento { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public bool EstaActivo { get; set; } = true;
    }
}
