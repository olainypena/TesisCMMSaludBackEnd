using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.Genero;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class GeneroService
    {
        private readonly ICommunRepository<Genero> _repository;

        public GeneroService(ICommunRepository<Genero> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(GeneroInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var Genero = new Genero(
                nombre: request.Nombre,
                codigo: request.Codigo,
                estaActivo: request.EstaActivo
            );

            await _repository.AddAsync(Genero, ct);
            await _repository.SaveChangesAsync(ct);

            return Genero.IdGenero;
        }

        public async Task<GeneroDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Genero = await _repository.GetByIdAsync(id, ct);
            return Genero is null ? null : MapToResponse(Genero);
        }

        public async Task<List<GeneroDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var Generos = await _repository.GetByAllAsync(ct);
            return Generos.Select(MapToResponse).ToList();
        }

        public async Task ActualizarGeneroAsync(int id, GeneroUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdGenero)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var Genero = await _repository.GetByIdAsync(request.IdGenero, ct);
            if (Genero is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdGenero, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            Genero.ActualizarGenero(
                request.IdGenero,
                request.Nombre,
                request.Codigo,              
                request.EstaActivo               
            );

            await _repository.UpdateAsync(Genero, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusGenero, CancellationToken ct = default)
        {
            var Genero = await _repository.GetByIdAsync(id, ct);
            if (Genero is null)
            {
                // No existe → false
                return false;
            }

            Genero.UpdateStatus(estatusGenero);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarGeneroAsync(int idGenero, CancellationToken ct = default)
        {
            var Genero = await _repository.GetByIdAsync(idGenero, ct);
            if (Genero is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idGenero, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(Genero, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static GeneroDetailDto MapToResponse(Genero Genero)
        {
            return new GeneroDetailDto
            {
                IdGenero = Genero.IdGenero,
                Nombre = Genero.Nombre,
                Codigo = Genero.Codigo,
                EstaActivo = Genero.EstaActivo
            };
        }
    }
}
