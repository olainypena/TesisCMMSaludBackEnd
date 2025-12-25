using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.TipoEquipamento;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class TipoEquipamentoService
    {
        private readonly ICommunRepository<TipoEquipamento> _repository;

        public TipoEquipamentoService(ICommunRepository<TipoEquipamento> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(TipoEquipamentoInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var TipoEquipamento = new TipoEquipamento(
                nombre: request.Nombre,
                descripcion: request.Descripcion,
                estaActivo: request.EstaActivo
            );

            await _repository.AddAsync(TipoEquipamento, ct);
            await _repository.SaveChangesAsync(ct);

            return TipoEquipamento.IdTipoEquipamento;
        }

        public async Task<TipoEquipamentoDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var TipoEquipamento = await _repository.GetByIdAsync(id, ct);
            return TipoEquipamento is null ? null : MapToResponse(TipoEquipamento);
        }

        public async Task<List<TipoEquipamentoDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var TipoEquipamentos = await _repository.GetByAllAsync(ct);
            return TipoEquipamentos.Select(MapToResponse).ToList();
        }

        public async Task ActualizarTipoEquipamentoAsync(int id, TipoEquipamentoUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdTipoEquipamento)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var TipoEquipamento = await _repository.GetByIdAsync(request.IdTipoEquipamento, ct);
            if (TipoEquipamento is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdTipoEquipamento, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            TipoEquipamento.ActualizarTipoEquipamento(
                request.IdTipoEquipamento,
                request.Nombre,
                request.Descripcion,              
                request.EstaActivo               
            );

            await _repository.UpdateAsync(TipoEquipamento, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusTipoEquipamento, CancellationToken ct = default)
        {
            var TipoEquipamento = await _repository.GetByIdAsync(id, ct);
            if (TipoEquipamento is null)
            {
                // No existe → false
                return false;
            }

            TipoEquipamento.UpdateStatus(estatusTipoEquipamento);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarTipoEquipamentoAsync(int idTipoEquipamento, CancellationToken ct = default)
        {
            var TipoEquipamento = await _repository.GetByIdAsync(idTipoEquipamento, ct);
            if (TipoEquipamento is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idTipoEquipamento, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(TipoEquipamento, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static TipoEquipamentoDetailDto MapToResponse(TipoEquipamento TipoEquipamento)
        {
            return new TipoEquipamentoDetailDto
            {
                IdTipoEquipamento = TipoEquipamento.IdTipoEquipamento,
                Nombre = TipoEquipamento.Nombre,
                Descripcion = TipoEquipamento.Descripcion,
                EstaActivo = TipoEquipamento.EstaActivo
            };
        }
    }
}
