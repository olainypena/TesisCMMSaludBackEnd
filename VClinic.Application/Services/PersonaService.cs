using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.Persona;
using VClinic.Domain.Entities;
using VClinic.Infrastructure.Repositories;

namespace VClinic.Application.Services
{
    public sealed class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<DatosPersona> GetOrCreatePersonaAsync(PersonaInsDto data, CancellationToken ct = default)
        {
            var tipoId = data.IdTipoIdentificacion;
            var identificacion = data.NumeroIdentificacion.Trim();

            var persona = await _personaRepository
                .GetByIdentificacionAsync(tipoId, identificacion, ct);

            if (persona is null)
            {
                persona = new DatosPersona(
                    data.Nombres,
                    data.Apellidos,
                    data.IdTipoIdentificacion,
                    data.NumeroIdentificacion,
                    data.IdEstadoCivil,
                    data.IdGenero,
                    data.FechaNacimiento,
                    data.Telefono,
                    data.Celular,
                    data.Email,
                    data.Direccion
                );

                await _personaRepository.AddAsync(persona, ct);
            }
            else
            {
                persona.ActualizarDatosBasicos(data.Nombres, data.Apellidos, data.FechaNacimiento);
                persona.ActualizarContacto(data.Telefono, data.Celular, data.Email, data.Direccion);
            }

            return persona;
        }

        public async Task UpdatePersonaAsync(DatosPersona persona, PersonaDto data, CancellationToken ct = default)
        {
            var otra = await _personaRepository
                .GetByIdentificacionAsync(data.IdTipoIdentificacion, data.NumeroIdentificacion.Trim(), ct);

            if (otra is not null && otra.IdPersona != persona.IdPersona)
                throw new ConflictException("Ya existe otra persona con la misma identificación.");

            persona.ActualizarDatosBasicos(data.Nombres, data.Apellidos, data.FechaNacimiento);
            persona.ActualizarIdentificacion(data.IdTipoIdentificacion, data.NumeroIdentificacion);
            persona.ActualizarContacto(data.Telefono, data.Celular, data.Email, data.Direccion);

            await _personaRepository.SaveChangesAsync(ct);
        }

        public async Task DeletePersonaAsync(long idpersona, CancellationToken ct = default)
        {
            if (await _personaRepository.HasRelatedDataAsync(idpersona))
                throw new ConflictException("No se puede eliminar la persona porque tiene datos relacionados.");

            var persona = await _personaRepository.GetByIdAsync(idpersona, ct);

            if (persona is null)
                throw new NotFoundException("La persona no fue encontrada.");

            await _personaRepository.DeleteAsync(persona, ct);
            await _personaRepository.SaveChangesAsync(ct);
        }
    }
}
