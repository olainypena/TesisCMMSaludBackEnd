using VClinic.Application.Common.Exceptions;
using VClinic.Application.Abstractions;
using VClinic.Application.DTOs.EstadoCivil;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class EstadoCivilService
    {
        private readonly IEstadoCivilRepository _repository;

        public EstadoCivilService(IEstadoCivilRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(EstadoCivilInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el estado civil porque el nombre o el RNC ya existe.");
            }

            var EstadoCivil = new EstadoCivil(                
                request.Nombre,
                request.EstaActivo
            );

            await _repository.AddAsync(EstadoCivil, ct);
            await _repository.SaveChangesAsync(ct);

            return EstadoCivil.IdEstadoCivil;
        }

        public async Task<EstadoCivilLstDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var EstadoCivil = await _repository.GetByIdAsync(id, ct);
            return EstadoCivil is null ? null : MapToResponse(EstadoCivil);
        }

        public async Task<List<EstadoCivilLstDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var centros = await _repository.GetByAllAsync(ct);
            return centros.Select(MapToResponse).ToList();
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusEstadoCivil, CancellationToken ct = default)
        {
            var EstadoCivil = await _repository.GetByIdAsync(id, ct);
            if (EstadoCivil is null)
            {
                // No existe → false
                return false;
            }

            EstadoCivil.UpdateStatus(estatusEstadoCivil);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }


        private static EstadoCivilLstDto MapToResponse(EstadoCivil estadoCivil)
        {
            return new EstadoCivilLstDto
            {
                IdEstadoCivil = estadoCivil.IdEstadoCivil,
                Nombre = estadoCivil.Nombre,
                EstaActivo = estadoCivil.EstaActivo
            };
        }

        public async Task ActualizarEstadoCivilAsync(int id, EstadoCivilUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdEstadoCivil)
                throw new BadRequestException("El estado civil que desea actualizar no coincide con el identificador enviado en la URL.");

            var EstadoCivil = await _repository.GetByIdAsync(request.IdEstadoCivil, ct);
            if (EstadoCivil is null)
            {
                throw new NotFoundException("estado civil no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdEstadoCivil, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el estado civil porque ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            EstadoCivil.ActualizarEstadoCivil(
                request.IdEstadoCivil,
                request.Nombre,               
                request.EstaActivo               
            );

            await _repository.UpdateAsync(EstadoCivil, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task EliminarEstadoCivilAsync(int idEstadoCivil, CancellationToken ct = default)
        {
            var EstadoCivil = await _repository.GetByIdAsync(idEstadoCivil, ct);
            if (EstadoCivil is null)
            {
                throw new NotFoundException("estado civil no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idEstadoCivil, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el estado civil porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(EstadoCivil, ct);
            await _repository.SaveChangesAsync(ct);
        }
    }
}
