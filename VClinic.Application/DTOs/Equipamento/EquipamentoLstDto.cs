using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.DTOs.Equipamento
{
    public class EquipamentoLstDto
    {
        public int IdEquipamento { get; set; }
        public string Nombre { get; set; } = default!;       
        public bool EstaActivo { get; set; } = true;
    }
}
