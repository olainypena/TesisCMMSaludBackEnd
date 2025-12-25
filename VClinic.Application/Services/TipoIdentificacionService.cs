using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.TipoIdentificacion;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class TipoIdentificacionService
    {
        private readonly ICommunRepository<TipoIdentificacion> _repository;

        public TipoIdentificacionService(ICommunRepository<TipoIdentificacion> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(TipoIdentificacionInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var TipoIdentificacion = new TipoIdentificacion(
                nombre: request.Nombre,
                codigo: request.Codigo,
                activo: request.EstaActivo
            );

            await _repository.AddAsync(TipoIdentificacion, ct);
            await _repository.SaveChangesAsync(ct);

            return TipoIdentificacion.IdTipoIdentificacion;
        }

        public async Task<TipoIdentificacionDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var TipoIdentificacion = await _repository.GetByIdAsync(id, ct);
            return TipoIdentificacion is null ? null : MapToResponse(TipoIdentificacion);
        }

        public async Task<List<TipoIdentificacionDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var TipoIdentificacions = await _repository.GetByAllAsync(ct);
            return TipoIdentificacions.Select(MapToResponse).ToList();
        }

        public async Task ActualizarTipoIdentificacionAsync(int id, TipoIdentificacionUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdTipoIdentificacion)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var TipoIdentificacion = await _repository.GetByIdAsync(request.IdTipoIdentificacion, ct);
            if (TipoIdentificacion is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdTipoIdentificacion, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            TipoIdentificacion.ActualizarDatos(
                request.IdTipoIdentificacion,
                request.Nombre,
                request.Codigo,              
                request.EstaActivo               
            );

            await _repository.UpdateAsync(TipoIdentificacion, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusTipoIdentificacion, CancellationToken ct = default)
        {
            var TipoIdentificacion = await _repository.GetByIdAsync(id, ct);
            if (TipoIdentificacion is null)
            {
                // No existe → false
                return false;
            }

            TipoIdentificacion.ActualizarEstatus(estatusTipoIdentificacion);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarTipoIdentificacionAsync(int idTipoIdentificacion, CancellationToken ct = default)
        {
            var TipoIdentificacion = await _repository.GetByIdAsync(idTipoIdentificacion, ct);
            if (TipoIdentificacion is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idTipoIdentificacion, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(TipoIdentificacion, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static TipoIdentificacionDetailDto MapToResponse(TipoIdentificacion TipoIdentificacion)
        {
            return new TipoIdentificacionDetailDto
            {
                IdTipoIdentificacion = TipoIdentificacion.IdTipoIdentificacion,
                Nombre = TipoIdentificacion.Nombre,
                Codigo = TipoIdentificacion.Codigo,
                EstaActivo = TipoIdentificacion.EstaActivo
            };
        }
    }
}
