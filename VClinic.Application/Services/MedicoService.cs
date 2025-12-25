// VClinic.Application/Services/MedicoService.cs
using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.Medico;
using VClinic.Application.DTOs.Persona;
using VClinic.Domain.Entities;
using VClinic.Infrastructure.Repositories;

namespace VClinic.Application.Services;

public sealed class MedicoService
{
    private readonly IPersonaService _personaService;
    private readonly IPersonaRepository _personaRepository;
    private readonly IMedicoRepository _medicoRepository;

    public MedicoService(
        IPersonaService personaService,
        IPersonaRepository personaRepository,
        IMedicoRepository medicoRepository)
    {
        _personaService = personaService;
        _personaRepository = personaRepository;
        _medicoRepository = medicoRepository;
    }

    // =========================
    // CREATE
    // =========================
    public async Task<long> CrearMedicoAsync(MedicoInsDto request, CancellationToken ct = default)
    {
        // 1. Obtener o crear Persona
        var persona = await _personaService.GetOrCreatePersonaAsync(request, ct);

        // 2. Validar si ya es médico
        if (await _medicoRepository.ExistsByPersonaIdAsync(persona.IdPersona, ct))
            throw new ConflictException("Esta persona ya está registrada como médico.");

        // 3. Validar número colegiado duplicado
        if (await _medicoRepository.ExistsByNroColegiadoAsync(0, request.NumeroColegiado, ct))
            throw new ConflictException("El número colegiado ya está registrado para otro médico.");

        // 4. Crear médico
        var medico = new Medico(
            0,
            persona,
            request.NumeroColegiado,
            request.Especialidad,
            request.Horario
        );

        await _medicoRepository.AddAsync(medico, ct);
        await _medicoRepository.SaveChangesAsync(ct);

        return medico.IdMedico;
    }

    // =========================
    // READ - LIST
    // =========================
    public async Task<List<MedicoLstDto>> GetAllAsync(CancellationToken ct = default)
    {
            var medicos = await _medicoRepository.GetAllWithPersonaAsync(ct);

        return medicos.Select(m => new MedicoLstDto
        {
            IdMedico = m.IdMedico,

            // Persona
            Nombres = m.Persona.Nombres,
            Apellidos = m.Persona.Apellidos,
            Telefono = m.Persona.Telefono,
            Celular = m.Persona.Celular,
            Email = m.Persona.Email,

            // Médico
            NumeroColegiado = m.NumeroColegiado,
            Especialidad = m.Especialidad,
            Horario = m.Horario,
            EstaActivo = m.EstaActivo

        }).ToList();
    }

    // =========================
    // READ - DETAIL
    // =========================
    public async Task<MedicoDetailDto?> GetByIdAsync(long idMedico, CancellationToken ct = default)
    {
        var medico = await _medicoRepository.GetByIdWithPersonaAsync(idMedico, ct);
        if (medico is null) return null;

        return new MedicoDetailDto
        {
            IdMedico = medico.IdMedico,

            // Persona
            Nombres = medico.Persona.Nombres,
            Apellidos = medico.Persona.Apellidos,
            FechaNacimiento = medico.Persona.FechaNacimiento,
            Telefono = medico.Persona.Telefono,
            Celular = medico.Persona.Celular,
            Email = medico.Persona.Email,
            Direccion = medico.Persona.Direccion,
            IdTipoIdentificacion = (int)medico.Persona.IdTipoIdentificacion,
            NumeroIdentificacion = medico.Persona.NumeroIdentificacion,

            // Médico
            NumeroColegiado = medico.NumeroColegiado,
            Especialidad = medico.Especialidad,
            Horario = medico.Horario,
            EstaActivo = medico.EstaActivo
        };
    }

    // =========================
    // UPDATE
    // =========================
    public async Task ActualizarMedicoAsync(long idMedico, MedicoUpdDto request, CancellationToken ct = default)
    {
        if (idMedico != request.IdMedico)
            throw new BadRequestException("El identificador del médico no coincide con el enviado en la URL.");

        var medico = await _medicoRepository.GetByIdWithPersonaAsync(idMedico, ct);
        if (medico is null)
            throw new NotFoundException("Médico no encontrado.");

        // Persona DTO
        var personaDto = new PersonaDto
        {
            IdPersona = medico.Persona.IdPersona,
            Nombres = medico.Persona.Nombres,
            Apellidos = medico.Persona.Apellidos,
            FechaNacimiento = medico.Persona.FechaNacimiento,
            Telefono = medico.Persona.Telefono,
            Celular = medico.Persona.Celular,
            Email = medico.Persona.Email,
            Direccion = medico.Persona.Direccion,
            IdTipoIdentificacion = (int)medico.Persona.IdTipoIdentificacion,
            NumeroIdentificacion = medico.Persona.NumeroIdentificacion
        };

        // Actualizar Persona
        await _personaService.UpdatePersonaAsync(medico.Persona, personaDto, ct);

        // Actualizar Médico
        medico.ActualizarDatosMedico(
            request.NumeroColegiado,
            request.Especialidad,
            request.Horario
        );

        medico.CambiarEstatus(request.EstaActivo);

        await _medicoRepository.UpdateAsync(medico, ct);
        await _medicoRepository.SaveChangesAsync(ct);
    }

    // =========================
    // DELETE
    // =========================
    public async Task EliminarMedicoAsync(long idMedico, CancellationToken ct = default)
    {
        var medico = await _medicoRepository.GetByIdWithPersonaAsync(idMedico, ct);
        if (medico is null)
            throw new NotFoundException("Médico no encontrado.");

        await _medicoRepository.DeleteAsync(medico, ct);
        await _medicoRepository.SaveChangesAsync(ct);

        await _personaService.DeletePersonaAsync(medico.IdPersona, ct);
    }
}
