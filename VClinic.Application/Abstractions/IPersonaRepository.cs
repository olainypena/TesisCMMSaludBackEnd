using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;

namespace VClinic.Infrastructure.Repositories
{
    public interface IPersonaRepository
    {
        Task<DatosPersona?> GetByIdentificacionAsync(int idtipoIdentificacion, string identificacion, CancellationToken ct = default);
        Task<DatosPersona?> GetByIdAsync(long idPersona, CancellationToken ct = default);
        Task AddAsync(DatosPersona persona, CancellationToken ct = default);
        Task UpdateAsync(DatosPersona persona, CancellationToken ct = default);
        Task DeleteAsync(DatosPersona persona, CancellationToken ct = default);

        Task<bool> HasRelatedDataAsync(long IdPersona, CancellationToken cancellationToken = default);

        Task<bool> ExistsDataAsync(long id, int idTipoIdentificacion, string identificacion, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
