using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Domain.Entities
{
    public class Paciente
    {
        public long IdPaciente { get; set; }
        public long IdPersona { get; set; }
        public DatosPersona Persona { get; set; } = null!;

        public int IdGrupoSangre { get; set; }
        public GrupoSanguinio GrupoSanguinio { get; set; } = null!;

        public int IdOcupacion {  get; set; }
        public Ocupacion Ocupacion { get; set; } = null!;

        public int IdHistorial { get; set; } = default!;
        public bool EstaActivo { get; set; } = true;

        private Paciente() { }

        public Paciente(DatosPersona persona, int historial, int idgrupoSangre)
        {
            Persona = persona ?? throw new ArgumentNullException(nameof(persona));
            ActualizarDatosPaciente(idgrupoSangre);
        }

        public void ActualizarDatosPaciente(int tipoSanguinio)
        {           
            IdGrupoSangre = tipoSanguinio;           
        }

        public void CambiarEstatus(bool estaActivo) => EstaActivo = estaActivo;
    }
}
