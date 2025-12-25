using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.Paciente;
using VClinic.Domain.Entities;

namespace VClinic.Application.Services
{

    public sealed class PacienteService
    {
        private readonly IPersonaService _personaService;
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(
            IPersonaService personaService,
            IPacienteRepository pacienteRepository)
        {
            _personaService = personaService;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<long> CrearPacienteAsync(PacienteInsDto request, CancellationToken ct = default)
        {
            // 1. Persona
            var persona = await _personaService.GetOrCreatePersonaAsync(request, ct);

            // 2. Ya es paciente?
            var yaEsPaciente = await _pacienteRepository.ExistsByPersonaIdAsync(persona.IdPersona, ct);
            if (yaEsPaciente)
                throw new ConflictException("Esta persona ya está registrada como paciente.");

            // 3. Crear paciente
            var paciente = new Paciente(persona, request.IdHistorial,request.IdGrupoSangre);

            await _pacienteRepository.AddAsync(paciente, ct);
            await _pacienteRepository.SaveChangesAsync(ct);

            return paciente.IdPaciente;
        }

        public async Task<List<PacienteLstDto>> GetAllAsync(CancellationToken ct = default)
        {
            var pacientes = await _pacienteRepository.GetAllWithPersonaAsync(ct);

            return pacientes.Select(p => new PacienteLstDto
            {
                IdPaciente = p.IdPaciente,
                Nombres = p.Persona.Nombres,
                Apellidos = p.Persona.Apellidos,
                Telefono = p.Persona.Telefono,
                Email = p.Persona.Email,
                
                IdHistorial = p.IdHistorial,
                EstaActivo = p.EstaActivo
            }).ToList();
        }

        public async Task<PacienteDetailDto?> GetByIdAsync(long idPaciente, CancellationToken ct = default)
        {
            var paciente = await _pacienteRepository.GetByIdWithPersonaAsync(idPaciente, ct);
            if (paciente is null) return null;

            return new PacienteDetailDto
            {
                IdPaciente = paciente.IdPaciente,
                Nombres = paciente.Persona.Nombres,
                Apellidos = paciente.Persona.Apellidos,
                FechaNacimiento = paciente.Persona.FechaNacimiento,
                Telefono = paciente.Persona.Telefono,
                Celular = paciente.Persona.Celular,
                Email = paciente.Persona.Email,
                Direccion = paciente.Persona.Direccion,
                IdTipoIdentificacion = (int)paciente.Persona.IdTipoIdentificacion,
                NumeroIdentificacion = paciente.Persona.NumeroIdentificacion,
                IdHistorial = paciente.IdHistorial,
                EstaActivo = paciente.EstaActivo
            };
        }

        public async Task ActualizarPacienteAsync(long idPaciente, PacienteUpdDto request, CancellationToken ct = default)
        {
            if (idPaciente != request.IdPaciente)
                throw new BadRequestException("El identificador del paciente no coincide con el enviado en la URL.");

            var paciente = await _pacienteRepository.GetByIdWithPersonaAsync(idPaciente, ct);
            if (paciente is null)
                throw new NotFoundException("Paciente no encontrado.");

            // Actualizar persona
            await _personaService.UpdatePersonaAsync(paciente.Persona, request, ct);

            // Actualizar datos de paciente
            paciente.ActualizarDatosPaciente(request.IdGrupoSangre);
            paciente.CambiarEstatus(request.EstaActivo);

            await _pacienteRepository.UpdateAsync(paciente, ct);
            await _pacienteRepository.SaveChangesAsync(ct);
        }

        public async Task EliminarPacienteAsync(long idPaciente, CancellationToken ct = default)
        {
            var paciente = await _pacienteRepository.GetByIdWithPersonaAsync(idPaciente, ct);
            if (paciente is null)
                throw new NotFoundException("Paciente no encontrado.");

            // Validar relaciones (citas, historiales, etc.) aquí si aplica

            await _pacienteRepository.DeleteAsync(paciente, ct);
            await _pacienteRepository.SaveChangesAsync(ct);
            await _personaService.DeletePersonaAsync(paciente.IdPersona, ct);
        }       
    }
}