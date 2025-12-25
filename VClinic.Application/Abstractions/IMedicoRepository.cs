using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.Abstractions
{
    public interface IMedicoRepository
    {
        Task<bool> ExistsByPersonaIdAsync(long idPersona, CancellationToken ct = default);
        Task<bool> ExistsByNroColegiadoAsync(long idMedico, string nroColegiado, CancellationToken ct = default);
        Task<Medico?> GetByIdWithPersonaAsync(long idMedico, CancellationToken ct = default);
        Task<List<Medico>> GetAllWithPersonaAsync(CancellationToken ct = default);

        Task AddAsync(Medico medico, CancellationToken ct = default);
        Task UpdateAsync(Medico medico, CancellationToken ct = default);
        Task DeleteAsync(Medico medico, CancellationToken ct = default);


        Task<bool> HasRelatedDataAsync(long idMedico, CancellationToken cancellationToken = default);

        Task<bool> ExistsDataAsync(long id, string NumeroColegiado, CancellationToken cancellationToken = default);


        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
