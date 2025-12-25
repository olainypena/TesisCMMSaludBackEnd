using VClinic.Application.Abstractions;
using VClinic.Application.Common.Exceptions;
using VClinic.Application.DTOs.CentroMedico;
using VClinic.Application.DTOs.Departamento;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Services
{
    public sealed class DepartamentoService
    {
        private readonly IDepartamentoRepository _repository;

        public DepartamentoService(IDepartamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(DepartamentoInsDto request, CancellationToken ct = default)
        {
            var registroDuplicado = await _repository.ExistsDataAsync(0, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                // 409 – Conflict
                throw new ConflictException("No se puede agregar el centro médico porque el nombre o el RNC ya existe.");
            }

            var Departamento = new Departamento(
                nombre: request.Nombre,
                codigo: request.Codigo,
                estaActivo: request.EstaActivo
            );

            await _repository.AddAsync(Departamento, ct);
            await _repository.SaveChangesAsync(ct);

            return Departamento.IdDepartamento;
        }

        public async Task<DepartamentoDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var Departamento = await _repository.GetByIdAsync(id, ct);
            return Departamento is null ? null : MapToResponse(Departamento);
        }

        public async Task<List<DepartamentoDetailDto>> GetByAllAsync(CancellationToken ct = default)
        {
            var departamentos = await _repository.GetByAllAsync(ct);
            return departamentos.Select(MapToResponse).ToList();
        }

        public async Task ActualizarDepartamentoAsync(int id, DepartamentoUpdDto request, CancellationToken ct = default)
        {
            if (id != request.IdDepartamento)
                throw new BadRequestException("El centro médico que desea actualizar no coincide con el identificador enviado en la URL.");

            var Departamento = await _repository.GetByIdAsync(request.IdDepartamento, ct);
            if (Departamento is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var registroDuplicado = await _repository.ExistsDataAsync(request.IdDepartamento, request.Nombre.Trim(), ct);
            if (registroDuplicado)
            {
                throw new ConflictException("No se puede actualizar el centro médico porque el nombre o el RNC ya existe.");
            }

            // Aquí deberías aplicar los cambios al objeto de dominio:
            Departamento.ActualizarDepartamento(
                request.IdDepartamento,
                request.Nombre,
                request.Codigo,              
                request.EstaActivo               
            );

            await _repository.UpdateAsync(Departamento, ct);
            await _repository.SaveChangesAsync(ct);
        }

        public async Task<bool> UpdateStatusAsync(int id, bool estatusDepartamento, CancellationToken ct = default)
        {
            var departamento = await _repository.GetByIdAsync(id, ct);
            if (departamento is null)
            {
                // No existe → false
                return false;
            }

            departamento.UpdateStatus(estatusDepartamento);
            await _repository.SaveChangesAsync(ct);

            // Se actualizó OK → true
            return true;
        }

        public async Task EliminarDepartamentoAsync(int idDepartamento, CancellationToken ct = default)
        {
            var Departamento = await _repository.GetByIdAsync(idDepartamento, ct);
            if (Departamento is null)
            {
                throw new NotFoundException("Centro médico no encontrado.");
            }

            var tieneRelacionados = await _repository.HasRelatedDataAsync(idDepartamento, ct);
            if (tieneRelacionados)
            {
                throw new ConflictException("No se puede eliminar el centro médico porque tiene registros asociados (sucursales, médicos, empleados o pacientes).");
            }

            await _repository.DeleteAsync(Departamento, ct);
            await _repository.SaveChangesAsync(ct);
        }

        private static DepartamentoDetailDto MapToResponse(Departamento departamento)
        {
            return new DepartamentoDetailDto
            {
                IdDepartamento = departamento.IdDepartamento,
                Nombre = departamento.Nombre,
                Codigo = departamento.Codigo,
                EstaActivo = departamento.EstaActivo
            };
        }
    }
}
