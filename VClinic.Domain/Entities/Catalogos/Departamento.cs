using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Entities.Catalogos
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EstaActivo { get; set; } = true;

        public ICollection<Empleado> Empleados { get; set; } = null!;

        public Departamento(string nombre, string codigo, bool estaActivo)
        {
            Nombre = nombre;
            Codigo = codigo;
            EstaActivo = estaActivo;
        }

        public void ActualizarDepartamento(int idDepartamento, string nombre, string codigo, bool estaActivo)
        {
            IdDepartamento = idDepartamento;
            Nombre = nombre;
            Codigo = codigo;
            EstaActivo = estaActivo;
        }

        public void UpdateStatus(bool estatusDepartamento)
        {
            EstaActivo = estatusDepartamento;
        }
    }
}
