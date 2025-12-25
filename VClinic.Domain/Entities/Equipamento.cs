using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Enums;

namespace VClinic.Domain.Entities
{
    public class Equipamento
    {
        public Equipamento(string nombre, string descripcion, int idTipoEquipo, bool estaActivo)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            IdTipoEquipo = idTipoEquipo;

            ActualizarEstatus(estaActivo);
        }

        public int IdEquipamento { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public int IdTipoEquipo { get; set; }
        public bool EstaActivo { get; set; } = true;    

        public void ActualizarEquipamento(int idEquipamento, string nombre, string descripcion, int idTipoEquipo, bool estaActivo)
        {
            IdEquipamento = idEquipamento;
            Nombre = nombre;
            Descripcion = descripcion;
            IdTipoEquipo = idTipoEquipo;
            ActualizarEstatus(estaActivo);
        }

        public void ActualizarEstatus(bool estatusEquipamento)
        {
            EstaActivo = estatusEquipamento;
        }
    }
}
