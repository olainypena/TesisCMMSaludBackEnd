using VClinic.Domain.Entities.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;

namespace VClinic.Application.Abstractions
{
    public interface IEstadoCivilRepository
    {
        Task<EstadoCivil?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<EstadoCivil>> GetByAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(EstadoCivil estadoCivil, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

       
        Task UpdateAsync(EstadoCivil estadoCivil, CancellationToken cancellationToken = default);
        Task DeleteAsync(EstadoCivil estadoCivil, CancellationToken cancellationToken = default);
                
        Task<bool> HasRelatedDataAsync(int estadoCivil, CancellationToken cancellationToken = default);

        Task<bool> ExistsDataAsync(int id, string estadoCivil, CancellationToken cancellationToken = default);

    }
}
