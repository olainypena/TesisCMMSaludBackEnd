using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.GrupoSanguinio;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class GrupoSanguinioService
    {
        private readonly ICommunRepository<GrupoSanguinio> _repository;

        public GrupoSanguinioService(ICommunRepository<GrupoSanguinio> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(GrupoSanguinioInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.TipoSangre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var GrupoSanguinio = new GrupoSanguinio(
                tipoSangre: request.TipoSangre,
                grupoABO: request.GrupoABO,
                factorRH: request.FactorRH,
                estaActivo: request.EstaActivo
            );

            await _repository.AddAsync(GrupoSanguinio, ct);
            await _repository.SaveChangesAsync(ct);

            return GrupoSanguinio.IdGrupoSangre;
        }

        public async Task<GrupoSanguinioDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var GrupoSanguinio = await _repository.GetByIdAsync(id, ct);
            return GrupoSanguinio is null ? null : MapToResponse(GrupoSanguinio);
        }

        public async Task<List<GrupoSanguinioDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var GrupoSanguinios = await _repository.GetByAllAsync(ct);
            return GrupoSanguinios.Select(MapToResponse).ToList();
        }

        public async Task ActualizarGrupoSanguinioAsync(int id, GrupoSanguinioUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdGrupoSangre)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var GrupoSanguinio = await _repository.GetByIdAsync(request.IdGrupoSangre, ct);
            if (GrupoSanguinio is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdGrupoSangre, request.TipoSangre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            GrupoSanguinio.ActualizarGrupoSanguinio(
                request.IdGrupoSangre,
                request.TipoSangre,
                request.GrupoABO,
                request.FactorRH,
                request.EstaActivo
            );

            await _repository.UpdateAsync(GrupoSanguinio, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusGrupoSanguinio, CancellationToken ct = default)
        {
            var GrupoSanguinio = await _repository.GetByIdAsync(id, ct);
            if (GrupoSanguinio is null)
            {
                // No existe → false
                return false;
            }

            GrupoSanguinio.UpdateStatus(estatusGrupoSanguinio);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarGrupoSanguinioAsync(int idGrupoSanguinio, CancellationToken ct = default)
        {
            var GrupoSanguinio = await _repository.GetByIdAsync(idGrupoSanguinio, ct);
            if (GrupoSanguinio is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idGrupoSanguinio, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(GrupoSanguinio, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static GrupoSanguinioDetailDto MapToResponse(GrupoSanguinio GrupoSanguinio)
        {
            return new GrupoSanguinioDetailDto
            {
                IdGrupoSangre = GrupoSanguinio.IdGrupoSangre,
                TipoSangre = GrupoSanguinio.TipoSangre,
                FactorRH = GrupoSanguinio.FactorRH,
                GrupoABO = GrupoSanguinio.GrupoABO,
                EstaActivo = GrupoSanguinio.EstaActivo
            };
        }
    }
}
