using VClinic.Application.Common.Exceptions;
using VClinic.Application.Abstractions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Domain.Entities;

namespace VClinic.Application.Services
{
    public sealed class CentroMedicoService
    {
        private readonly ICentroMedicoRepository _repository;

        public CentroMedicoService(ICentroMedicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CentroMedicoDto> CreateAsync(CentroMedicoInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), request.Rnc.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var centroMedico = new CentroMedico(
                0,
                request.Nombre,
                request.Descripcion,
                request.Direccion,
                request.Telefono,
                request.EstaActivo,
                request.FechaFundacion,
                request.Rnc,
                request.Tipo,
                request.Codigo
            );

            await _repository.AddAsync(centroMedico, ct);
            await _repository.SaveChangesAsync(ct);

            return MapToResponse(centroMedico);
        }

        public async Task<CentroMedicoDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var centroMedico = await _repository.GetByIdAsync(id, ct);
            return centroMedico is null ? null : MapToResponse(centroMedico);
        }

        public async Task<List<CentroMedicoDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var centros = await _repository.GetByAllAsync(ct);
            return centros.Select(MapToResponse).ToList();
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusCentroMedico, CancellationToken ct = default)
        {
            var centroMedico = await _repository.GetByIdAsync(id, ct);
            if (centroMedico is null)
            {
                // No existe → false
                return false;
            }

            centroMedico.UpdateStatus(estatusCentroMedico);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }


        private static CentroMedicoDto MapToResponse(CentroMedico centroMedico)
        {
            return new CentroMedicoDto
            {
                IdCentroMedico = centroMedico.IdCentroMedico,
                Nombre = centroMedico.Nombre,
                Descripcion = centroMedico.Descripcion,
                Direccion = centroMedico.Direccion,
                Telefono = centroMedico.Telefono,
                EstaActivo = centroMedico.EstaActivo,
                FechaFundacion = centroMedico.FechaFundacion,
                Rnc = centroMedico.Rnc,
                Tipo = centroMedico.Tipo,
                Codigo = centroMedico.Codigo
            };
        }

        public async Task ActualizarCentroMedicoAsync(int id, CentroMedicoDto request, CancellationToken ct = default)
        {
            if (id != request.IdCentroMedico)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var centroMedico = await _repository.GetByIdAsync(request.IdCentroMedico, ct);
            if (centroMedico is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdCentroMedico, request.Nombre.Trim(), request.Rnc.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            centroMedico.ActualizarCentroMedico(
                request.IdCentroMedico,
                request.Nombre,
                request.Descripcion,
                request.Direccion,
                request.Telefono,
                request.EstaActivo,
                request.FechaFundacion,
                request.Rnc,
                request.Tipo,
                request.Codigo
            );

            await _repository.UpdateAsync(centroMedico, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task EliminarCentroMedicoAsync(int idCentroMedico, CancellationToken ct = default)
        {
            var centroMedico = await _repository.GetByIdAsync(idCentroMedico, ct);
            if (centroMedico is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idCentroMedico, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(centroMedico, ct);
            await _repository.SaveChangesAsync(ct);
        }
    }
}
