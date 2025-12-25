using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.Ocupacion;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class OcupacionService
    {
        private readonly ICommunRepository<Ocupacion> _repository;

        public OcupacionService(ICommunRepository<Ocupacion> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(OcupacionInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var Ocupacion = new Ocupacion(
                nombre: request.Nombre,
                estaActivo: request.EstaActivo
            );

            await _repository.AddAsync(Ocupacion, ct);
            await _repository.SaveChangesAsync(ct);

            return Ocupacion.IdOcupacion;
        }

        public async Task<OcupacionDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Ocupacion = await _repository.GetByIdAsync(id, ct);
            return Ocupacion is null ? null : MapToResponse(Ocupacion);
        }

        public async Task<List<OcupacionDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var Ocupacions = await _repository.GetByAllAsync(ct);
            return Ocupacions.Select(MapToResponse).ToList();
        }

        public async Task ActualizarOcupacionAsync(int id, OcupacionUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdOcupacion)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var Ocupacion = await _repository.GetByIdAsync(request.IdOcupacion, ct);
            if (Ocupacion is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdOcupacion, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            Ocupacion.ActualizarOcupacion(
                request.IdOcupacion,
                request.Nombre,                         
                request.EstaActivo               
            );

            await _repository.UpdateAsync(Ocupacion, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusOcupacion, CancellationToken ct = default)
        {
            var Ocupacion = await _repository.GetByIdAsync(id, ct);
            if (Ocupacion is null)
            {
                // No existe → false
                return false;
            }

            Ocupacion.UpdateStatus(estatusOcupacion);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarOcupacionAsync(int idOcupacion, CancellationToken ct = default)
        {
            var Ocupacion = await _repository.GetByIdAsync(idOcupacion, ct);
            if (Ocupacion is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idOcupacion, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(Ocupacion, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static OcupacionDetailDto MapToResponse(Ocupacion Ocupacion)
        {
            return new OcupacionDetailDto
            {
                IdOcupacion = Ocupacion.IdOcupacion,
                Nombre = Ocupacion.Nombre,              
                EstaActivo = Ocupacion.EstaActivo
            };
        }
    }
}
