using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class TipoEquipamento
    {
        public TipoEquipamento(string nombre, string descripcion, bool estaActivo)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            UpdateStatus(estaActivo);
        }

        public int IdTipoEquipamento { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;       
        public bool EstaActivo { get; set; } = true;

        public void ActualizarTipoEquipamento(int idTipoEquipamento, string nombre, string descripcion, bool estaActivo)
        {
            IdTipoEquipamento = idTipoEquipamento;
            Nombre = nombre;
            Descripcion = descripcion;
            UpdateStatus(estaActivo);
        }

        public void UpdateStatus(bool estatusTipoEquipamento)
        {
            EstaActivo = estatusTipoEquipamento;
        }
    }
}
