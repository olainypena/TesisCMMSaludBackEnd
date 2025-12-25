using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;

namespace VClinic.Application.Abstractions
{
    public interface IEmpleadoRepository
    {
        Task<bool> ExistsByPersonaIdAsync(long idPersona, CancellationToken ct = default);
        Task<Empleado?> GetByIdWithPersonaAsync(long idEmpleado, CancellationToken ct = default);
        Task<List<Empleado>> GetAllWithPersonaAsync(CancellationToken ct = default);

        Task AddAsync(Empleado empleado, CancellationToken ct = default);
        Task UpdateAsync(Empleado empleado, CancellationToken ct = default);
        Task DeleteAsync(Empleado empleado, CancellationToken ct = default);

        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
