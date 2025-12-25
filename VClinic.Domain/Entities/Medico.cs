using System;
using VClinic.Domain.Entities;

public class Medico
{
    public long IdMedico { get; set; }
    public long IdPersona { get; private set; }

    public string NumeroColegiado { get; private set; } = default!;
    public string Especialidad { get; private set; } = default!;
    public string Horario { get; private set; } = default!;

    public bool EstaActivo { get; private set; } = true;

    public DatosPersona Persona { get; private set; } = default!;

    protected Medico() { } // EF Core

    // ✅ CONSTRUCTOR COMPLETO (USADO EN CREATE)
    public Medico(
        long idMedico,
        DatosPersona persona,
        string numeroColegiado,
        string especialidad,
        string horario)
    {
        IdMedico = idMedico;
        Persona = persona ?? throw new ArgumentNullException(nameof(persona));
        IdPersona = persona.IdPersona;

        ActualizarDatosMedico(numeroColegiado, especialidad, horario);
    }

    // 🔄 MANTENER COMPATIBILIDAD (POR SI YA EXISTE USO)
    public Medico(long idMedico, DatosPersona persona, string numeroColegiado)
    {
        IdMedico = idMedico;
        Persona = persona ?? throw new ArgumentNullException(nameof(persona));
        IdPersona = persona.IdPersona;

        ActualizarDatosMedico(numeroColegiado);
    }

    // ✅ MÉTODO COMPLETO (UPDATE)
    public void ActualizarDatosMedico(
        string numeroColegiado,
        string especialidad,
        string horario)
    {
        if (string.IsNullOrWhiteSpace(numeroColegiado))
            throw new ArgumentException("El número de colegiado es requerido.", nameof(numeroColegiado));

        if (string.IsNullOrWhiteSpace(especialidad))
            throw new ArgumentException("La especialidad es requerida.", nameof(especialidad));

        if (string.IsNullOrWhiteSpace(horario))
            throw new ArgumentException("El horario es requerido.", nameof(horario));

        NumeroColegiado = numeroColegiado.Trim();
        Especialidad = especialidad.Trim();
        Horario = horario.Trim();
    }

    // 🔄 MÉTODO ANTIGUO (NO SE ROMPE NADA)
    public void ActualizarDatosMedico(string numeroColegiado)
    {
        if (string.IsNullOrWhiteSpace(numeroColegiado))
            throw new ArgumentException("El número de colegiado es requerido.", nameof(numeroColegiado));

        NumeroColegiado = numeroColegiado.Trim();
    }

    public void CambiarEstatus(bool estaActivo) => EstaActivo = estaActivo;
}
