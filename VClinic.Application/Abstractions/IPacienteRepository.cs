using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;

namespace VClinic.Application.Abstractions
{
    public interface IPacienteRepository
    {
        Task<bool> ExistsByPersonaIdAsync(long idPersona, CancellationToken ct = default);
        Task<Paciente?> GetByIdWithPersonaAsync(long idPaciente, CancellationToken ct = default);
        Task<List<Paciente>> GetAllWithPersonaAsync(CancellationToken ct = default);

        Task AddAsync(Paciente paciente, CancellationToken ct = default);
        Task UpdateAsync(Paciente paciente, CancellationToken ct = default);
        Task DeleteAsync(Paciente paciente, CancellationToken ct = default);

        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
