using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.Posicion;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class PosicionService
    {
        private readonly ICommunRepository<Posiciones> _repository;

        public PosicionService(ICommunRepository<Posiciones> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(PosicionInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var Posicion = new Posiciones(
                nombre: request.Nombre,                
                nivel: request.Nivel,
                estaActivo: request.EstaActivo
            );

            await _repository.AddAsync(Posicion, ct);
            await _repository.SaveChangesAsync(ct);

            return Posicion.IdPosicion;
        }

        public async Task<PosicionDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Posicion = await _repository.GetByIdAsync(id, ct);
            return Posicion is null ? null : MapToResponse(Posicion);
        }

        public async Task<List<PosicionDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var Posicions = await _repository.GetByAllAsync(ct);
            return Posicions.Select(MapToResponse).ToList();
        }

        public async Task ActualizarPosicionAsync(int id, PosicionUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdPosicion)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var Posicion = await _repository.GetByIdAsync(request.IdPosicion, ct);
            if (Posicion is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdPosicion, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            Posicion.ActualizarPosicion(
                request.IdPosicion,
                request.Nombre,
                request.Nivel,              
                request.EstaActivo               
            );

            await _repository.UpdateAsync(Posicion, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusPosicion, CancellationToken ct = default)
        {
            var Posicion = await _repository.GetByIdAsync(id, ct);
            if (Posicion is null)
            {
                // No existe → false
                return false;
            }

            Posicion.UpdateStatus(estatusPosicion);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarPosicionAsync(int idPosicion, CancellationToken ct = default)
        {
            var Posicion = await _repository.GetByIdAsync(idPosicion, ct);
            if (Posicion is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idPosicion, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(Posicion, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static PosicionDetailDto MapToResponse(Posiciones Posicion)
        {
            return new PosicionDetailDto
            {
                IdPosicion = Posicion.IdPosicion,
                Nombre = Posicion.Nombre,
                Nivel = Posicion.Nivel,
                EstaActivo = Posicion.EstaActivo
            };
        }
    }
}
