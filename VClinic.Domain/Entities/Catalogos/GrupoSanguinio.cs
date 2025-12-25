using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class GrupoSanguinio
    {        
        public GrupoSanguinio(string tipoSangre, string grupoABO, string factorRH, bool estaActivo)
        {
            TipoSangre = tipoSangre;
            GrupoABO = grupoABO;
            FactorRH = factorRH;
            EstaActivo = estaActivo;
        }

        [Required(ErrorMessage = "El campo {0} es requerido.")]        
        public int IdGrupoSangre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Tipo de Sangre")]
        [StringLength(3)]
        public string TipoSangre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Grupo ABO")]
        [StringLength(2)]
        public string GrupoABO { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Factor RH")]
        [StringLength(10)]
        public string FactorRH { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool EstaActivo { get; private set; } = true;

        public ICollection<Paciente> Pacientes { get; set; } = null!;

        public void ActualizarGrupoSanguinio(int idGrupoSangre, string tipoSangre, string grupoABO, string factorRH, bool estaActivo)
        {
            IdGrupoSangre = idGrupoSangre;
            TipoSangre = tipoSangre;
            GrupoABO = grupoABO;
            FactorRH = factorRH;
            EstaActivo = estaActivo;

        }

        public void UpdateStatus(bool estatusGrupoSanguinio)
        {
           EstaActivo = estatusGrupoSanguinio;
        }
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