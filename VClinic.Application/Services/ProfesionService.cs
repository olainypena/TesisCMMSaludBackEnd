using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.Profesion;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class ProfesionService
    {
        private readonly ICommunRepository<Profesion> _repository;

        public ProfesionService(ICommunRepository<Profesion> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(ProfesionInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var Profesion = new Profesion(
                nombre: request.Nombre,
                estaActivo: request.EstaActivo
            );

            await _repository.AddAsync(Profesion, ct);
            await _repository.SaveChangesAsync(ct);

            return Profesion.IdProfesion;
        }

        public async Task<ProfesionDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Profesion = await _repository.GetByIdAsync(id, ct);
            return Profesion is null ? null : MapToResponse(Profesion);
        }

        public async Task<List<ProfesionDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var Profesions = await _repository.GetByAllAsync(ct);
            return Profesions.Select(MapToResponse).ToList();
        }

        public async Task ActualizarProfesionAsync(int id, ProfesionUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdProfesion)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var Profesion = await _repository.GetByIdAsync(request.IdProfesion, ct);
            if (Profesion is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdProfesion, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            Profesion.ActualizarProfesion(
                request.IdProfesion,
                request.Nombre,                            
                request.EstaActivo               
            );

            await _repository.UpdateAsync(Profesion, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusProfesion, CancellationToken ct = default)
        {
            var Profesion = await _repository.GetByIdAsync(id, ct);
            if (Profesion is null)
            {
                // No existe → false
                return false;
            }

            Profesion.ActualizarEstatus(estatusProfesion);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarProfesionAsync(int idProfesion, CancellationToken ct = default)
        {
            var Profesion = await _repository.GetByIdAsync(idProfesion, ct);
            if (Profesion is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idProfesion, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(Profesion, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static ProfesionDetailDto MapToResponse(Profesion Profesion)
        {
            return new ProfesionDetailDto
            {
                IdProfesion = Profesion.IdProfesion,
                Nombre = Profesion.Nombre,               
                EstaActivo = Profesion.EstaActivo
            };
        }
    }
}
