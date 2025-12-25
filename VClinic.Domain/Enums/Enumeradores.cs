using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Domain.Enums
{
    public enum TipoCentroMedicoEnum
    {
        Privado = 1, Publico = 2, Semiprivado = 3, SinDefinir = 4, Otro = 5
    }

    public enum CategoriaEquipoEnum
    {
        Medico = 1, Oficina = 2, Otro = 3
    }

    public enum EstadoCitaEnum
    {
        Programada = 1, Completada = 2, Cancelada = 3, NoAsistio = 4, Reprogramada = 5, NoAtendida = 6
    }

    public enum GeneroEnum
    {
        Masculino = 1, Femenino = 2, Otro = 3, NoEspecificado = 4
    }

    public enum TipoIdentificacionEnum
    {
        Cedula = 1, Pasaporte = 2, Ruc = 3, Otro = 4
    }
    public enum EstadoCivilEnum
    {
        Soltero = 1, Casado = 2, Divorciado = 3, Viudo = 4, UnionLibre = 5
    }

    
}
