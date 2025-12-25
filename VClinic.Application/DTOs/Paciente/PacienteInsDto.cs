using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.DTOs.Persona;

namespace VClinic.Application.DTOs.Paciente
{
    public class PacienteInsDto: PersonaInsDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Tipo de Sangre")]
        public int IdGrupoSangre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Historial Medico")]
        public int IdHistorial { get; private set; } = default!;
        public bool EstaActivo { get; private set; } = true;
    }
}



//ID_TipoSangre Tipo_Completo	Grupo_ABO	Factor_Rh
//1	A+	A	Positivo
//2	A-	A	Negativo
//3	B+	B	Positivo
//4	B-	B	Negativo
//5	O+	O	Positivo
//6	O-	O	Negativo
//7	AB+	AB	Positivo
//8	AB-	AB	Negativo