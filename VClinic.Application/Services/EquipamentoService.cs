using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.Equipamento;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class EquipamentoService
    {
        private readonly ICommunRepository<Equipamento> _repository;

        public EquipamentoService(ICommunRepository<Equipamento> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(EquipamentoInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var Equipamento = new Equipamento(
                nombre: request.Nombre, 
                descripcion: request.Descripcion,
                idTipoEquipo: request.IdTipoEquipo,
                estaActivo: request.EstaActivo
            );

            await _repository.AddAsync(Equipamento, ct);
            await _repository.SaveChangesAsync(ct);

            return Equipamento.IdEquipamento;
        }

        public async Task<EquipamentoDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Equipamento = await _repository.GetByIdAsync(id, ct);
            return Equipamento is null ? null : MapToResponse(Equipamento);
        }

        public async Task<List<EquipamentoDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var Equipamentos = await _repository.GetByAllAsync(ct);
            return Equipamentos.Select(MapToResponse).ToList();
        }

        public async Task ActualizarEquipamentoAsync(int id, EquipamentoUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdEquipamento)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var Equipamento = await _repository.GetByIdAsync(request.IdEquipamento, ct);
            if (Equipamento is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdEquipamento, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            Equipamento.ActualizarEquipamento(
                request.IdEquipamento,
                request.Nombre,   
                request.Descripcion,
                request.IdTipoEquipo,
                request.EstaActivo               
            );

            await _repository.UpdateAsync(Equipamento, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusEquipamento, CancellationToken ct = default)
        {
            var Equipamento = await _repository.GetByIdAsync(id, ct);
            if (Equipamento is null)
            {
                // No existe → false
                return false;
            }

            Equipamento.ActualizarEstatus(estatusEquipamento);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarEquipamentoAsync(int idEquipamento, CancellationToken ct = default)
        {
            var Equipamento = await _repository.GetByIdAsync(idEquipamento, ct);
            if (Equipamento is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idEquipamento, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(Equipamento, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static EquipamentoDetailDto MapToResponse(Equipamento Equipamento)
        {
            return new EquipamentoDetailDto
            {
                IdEquipamento = Equipamento.IdEquipamento,
                Nombre = Equipamento.Nombre,               
                EstaActivo = Equipamento.EstaActivo
            };
        }
    }
}
