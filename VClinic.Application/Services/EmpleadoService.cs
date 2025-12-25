using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.Empleado;
using VClinic.Domain.Entities;
using VClinic.Domain.Enums;

namespace VClinic.Application.Services
{
    public sealed class EmpleadoService
    {
        private readonly IPersonaService _personaService;
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService(
            IPersonaService personaService,
            IEmpleadoRepository empleadoRepository)
        {
            _personaService = personaService;
            _empleadoRepository = empleadoRepository;
        }

        public async Task<long> CrearEmpleadoAsync(EmpleadoInsDto request, CancellationToken ct = default)
        {
            var persona = await _personaService.GetOrCreatePersonaAsync(request, ct);

            var yaEsEmpleado = await _empleadoRepository.ExistsByPersonaIdAsync(persona.IdPersona, ct);
            if (yaEsEmpleado)
                throw new ConflictException("Esta persona ya está registrada como empleado.");

            var empleado = new Empleado(persona, request.NumeroEmpleado, request.IdCargo);

            await _empleadoRepository.AddAsync(empleado, ct);
            await _empleadoRepository.SaveChangesAsync(ct);

            return empleado.IdEmpleado;
        }

        public async Task<List<EmpleadoLstDto>> GetAllAsync(CancellationToken ct = default)
        {
            var empleados = await _empleadoRepository.GetAllWithPersonaAsync(ct);

            return empleados.Select(e => new EmpleadoLstDto
            {
                IdEmpleado = e.IdEmpleado,
                Nombres = e.Persona.Nombres,
                Apellidos = e.Persona.Apellidos,
                Telefono = e.Persona.Telefono,
                Email = e.Persona.Email,
                NumeroEmpleado = e.CodigoEmpleado,
                IdCargo = e.IdPosicion,
                EstaActivo = e.EstaActivo
            }).ToList();
        }

        public async Task<EmpleadoDetailDto?> GetByIdAsync(long idEmpleado, CancellationToken ct = default)
        {
            var empleado = await _empleadoRepository.GetByIdWithPersonaAsync(idEmpleado, ct);
            if (empleado is null) return null;

            return new EmpleadoDetailDto
            {
                IdEmpleado = empleado.IdEmpleado,
                Nombres = empleado.Persona.Nombres,
                Apellidos = empleado.Persona.Apellidos,
                FechaNacimiento = empleado.Persona.FechaNacimiento,
                Telefono = empleado.Persona.Telefono,
                Celular = empleado.Persona.Celular,
                Email = empleado.Persona.Email,
                Direccion = empleado.Persona.Direccion,
                IdTipoIdentificacion = (int)empleado.Persona.IdTipoIdentificacion,
                NumeroIdentificacion = empleado.Persona.NumeroIdentificacion,
                NumeroEmpleado = empleado.CodigoEmpleado,
                IdCargo = empleado.IdPosicion,
                EstaActivo = empleado.EstaActivo
            };
        }

        public async Task ActualizarEmpleadoAsync(long idEmpleado, EmpleadoUpdDto request, CancellationToken ct = default)
        {
            if (idEmpleado != request.IdEmpleado)
                throw new BadRequestException("El identificador del empleado no coincide con el enviado en la URL.");

            var empleado = await _empleadoRepository.GetByIdWithPersonaAsync(idEmpleado, ct);
            if (empleado is null)
                throw new NotFoundException("Empleado no encontrado.");

            await _personaService.UpdatePersonaAsync(empleado.Persona, request, ct);

            empleado.ActualizarDatosEmpleado(request.NumeroEmpleado, request.IdCargo);
            empleado.CambiarEstatus(request.EstaActivo);

            await _empleadoRepository.UpdateAsync(empleado, ct);
            await _empleadoRepository.SaveChangesAsync(ct);
        }

        public async Task EliminarEmpleadoAsync(long idEmpleado, CancellationToken ct = default)
        {
            var empleado = await _empleadoRepository.GetByIdWithPersonaAsync(idEmpleado, ct);
            if (empleado is null)
                throw new NotFoundException("Empleado no encontrado.");

            // Validar relaciones (nómina, usuarios, etc.) aquí si aplica

            await _empleadoRepository.DeleteAsync(empleado, ct);
            await _empleadoRepository.SaveChangesAsync(ct);
            await _personaService.DeletePersonaAsync(empleado.IdPersona, ct);
        }
    }
}